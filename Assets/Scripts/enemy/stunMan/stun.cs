using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class stun : MonoBehaviour
{
    public float deleteTime = 20f;
    public float speed = 7f;
    public float speedNerf;
    private Vector3 direction;
    private Vector3 targetPosition;
    private Vector3 startLocation;
    private float range;
    private float angle;
    private GameObject player;
    public float stunDuration = 5f;
    private bool hasHitPlayer = false;
    public GameObject parent;
    public void SetCreator(GameObject creatorObject)
    {
        parent = creatorObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        startLocation = transform.position;

        Destroy(gameObject,deleteTime);

        player = GameObject.FindGameObjectWithTag("character");
        targetPosition = player.transform.position;

        // Ignore the Z-axis to ensure the object stays in the 2D plane
        targetPosition.z = transform.position.z;

        // Calculate the direction towards the mouse position
        direction = (targetPosition - transform.position).normalized;

        // Shoot the object towards the mouse position
        ShootObject(direction);

        RotateObject(direction);
    }
      void RotateObject(Vector3 direction)
    {
        // Calculate the angle in degrees
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the object
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
      void ShootObject(Vector3 direction)
    {
        // Assuming the object has a Rigidbody2D component
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Apply force to the object in the calculated direction
        rb.velocity = direction * speed;
    }
    private void stunResetDelete()
    {
        characterStats cStats = player.GetComponent<characterStats>();
        cStats.movementBuff += speedNerf;
        Destroy(gameObject);
    }

 
     public void  OnTriggerEnter2D(Collider2D other){

         if(other.gameObject.tag == "character" && !hasHitPlayer){
            
            hasHitPlayer = true;
            characterStats cStats = player.GetComponent<characterStats>();
            cStats.movementBuff -= speedNerf;
            stunMan sStats = parent.GetComponent<stunMan>();
            sStats.hitTarget = true;
            sStats.StartAttackTimer();        
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);
                Invoke("stunResetDelete",stunDuration);
        }  
    }
}
