using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public float deleteTime = 10f;
    public float speed = 7f;
    public int dmg = 15;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
         Destroy(gameObject,deleteTime);
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

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
     public void  OnTriggerEnter2D(Collider2D other){
         if(other.gameObject.tag == "enemy"){
            enemyStats eEnemy = other.gameObject.GetComponent<enemyStats>();
            eEnemy.takeDamage(dmg);
         }
         if(other.gameObject.tag != "character"){
            Destroy(gameObject);
         }
        
    }

}
