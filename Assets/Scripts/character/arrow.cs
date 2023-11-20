using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    // Prefab for the arrow explosion effect
    public GameObject explo;

    // Time before the arrow is automatically destroyed
    public float deleteTime = 10f;

    // Speed of the arrow
    public float speed = 7f;

    // Damage inflicted by the arrow
    private int dmg = 15;

    // Direction towards the target
    private Vector3 direction;

    // Target position for the arrow
    private Vector3 targetPosition;

    // Starting position of the arrow
    private Vector3 startLocation;

    // Maximum range the arrow can travel
    private float range;

    // Angle of rotation for the arrow
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        // Set the starting position of the arrow
        startLocation = transform.position;

        // Retrieve adjusted attack and range values from the characterStats component
        dmg = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().adjAtk;
        range = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().range;

        // Destroy the arrow after a specified time
        Destroy(gameObject, deleteTime);

        // Get the target position based on the mouse position
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Ignore the Z-axis to ensure the object stays in the 2D plane
        targetPosition.z = transform.position.z;

        // Calculate the direction towards the mouse position
        direction = (targetPosition - transform.position).normalized;

        // Shoot the arrow towards the mouse position
        ShootObject(direction);

        // Rotate the arrow based on its direction
        RotateObject(direction);
    }

    // Rotate the arrow based on its direction
    void RotateObject(Vector3 direction)
    {
        // Calculate the angle in degrees
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the arrow
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Shoot the arrow in a given direction
    void ShootObject(Vector3 direction)
    {
        // Assuming the object has a Rigidbody2D component
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Apply force to the arrow in the calculated direction
        rb.velocity = direction * speed;
    }

    // Handle collisions with other objects
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

        // Check if the arrow collides with an object that is not the player or untagged
        if (other.gameObject.tag != "character" && other.gameObject.tag != "Untagged")
        {
            // Destroy the arrow
            Destroy(gameObject);

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

        // Destroy the arrow if it exceeds its maximum range
        if (distance > range)
        {
            Destroy(gameObject);
        }
    }
}