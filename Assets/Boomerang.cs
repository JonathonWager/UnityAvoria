using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public GameObject explo;
    private bool startReturn = false;
    private GameObject player;
     private Vector3 direction;

    // Target position for the arrow
    private Vector3 targetPosition;

    // Starting position of the arrow
    private Vector3 startLocation;

    
    public float speed = 7f;

    // Damage inflicted by the arrow
    private int dmg;
    private float range;

    // Angle of rotation for the arrow
    private float angle;
    private float slowdownDistance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("character");
        startLocation = transform.position;
        dmg = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().adjAtk;
        range = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().range;
        slowdownDistance = range/2;
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

          // Ignore the Z-axis to ensure the object stays in the 2D plane
        targetPosition.z = transform.position.z;

        // Calculate the direction towards the mouse position
        direction = (targetPosition - transform.position).normalized;

        ShootObject(direction);

        // Rotate the arrow based on its direction
        RotateObject(direction);
    }
      void RotateObject(Vector3 direction)
    {
        // Calculate the angle in degrees
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the arrow
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
     void ShootObject(Vector3 direction)
    {
        // Assuming the object has a Rigidbody2D component
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Apply force to the arrow in the calculated direction
        rb.velocity = direction * speed;
    }
     public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the arrow collides with an enemy
        if (other.gameObject.tag == "enemy")
        {
            // Inflict damage to the enemy
            enemyStats eEnemy = other.gameObject.GetComponent<enemyStats>();
            eEnemy.takeDamage(dmg);

            // Instantiate the explosion effect at the arrow's position
            Quaternion finalRotation = transform.rotation * Quaternion.Euler(0f, 90f, 0f);
            Instantiate(explo, transform.position, finalRotation);
        }

    }
    // Update is called once per frame
    void Update()
    {
         // Calculate the distance traveled by the arrow
        float distance = Vector3.Distance(startLocation, transform.position);
        if(distance > slowdownDistance){
           // Rigidbody2D rb = GetComponent<Rigidbody2D>();

            // Apply force to the arrow in the calculated direction
            //speed = speed * 0.9f;
           // rb.velocity = direction * 0f;
            //rb.velocity = direction * speed;
        }
        if (distance > range)
        {
            startReturn = true;
             Rigidbody2D rb = GetComponent<Rigidbody2D>();
              rb.velocity = direction * 0f;
        }
        if(startReturn){
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if(player.transform.position == this.transform.position){
                Destroy(gameObject);
            }
        }
    }
}
