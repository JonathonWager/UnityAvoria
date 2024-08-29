using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public GameObject explo;
    private bool startReturn = false;
    private GameObject player;
    private Vector3 direction;

    // Target position for the boomerang
    private Vector3 targetPosition;

    // Starting position of the boomerang
    private Vector3 startLocation;

    public float speed = 7f;

    // Damage inflicted by the boomerang
    private int dmg;
    private float range;

    // Knockback force
    public float knockbackForce = 5f;

    private float angle;
    private float slowdownDistance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("character");
        startLocation = transform.position;
        dmg = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().adjAtk;
        range = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().range;
        slowdownDistance = range / 2;
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Ignore the Z-axis to ensure the object stays in the 2D plane
        targetPosition.z = transform.position.z;

        // Calculate the direction towards the mouse position
        direction = (targetPosition - transform.position).normalized;

        ShootObject(direction);

        // Rotate the boomerang based on its direction
        RotateObject(direction);
    }

    void RotateObject(Vector3 direction)
    {
        // Calculate the angle in degrees
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the boomerang
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void ShootObject(Vector3 direction)
    {
        // Assuming the object has a Rigidbody2D component
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Apply force to the boomerang in the calculated direction
        rb.velocity = direction * speed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the boomerang collides with an enemy
        if (other.gameObject.tag == "enemy")
        {
            // Inflict damage and apply knockback to the enemy
            enemyStats eEnemy = other.gameObject.GetComponent<enemyStats>();
            if (eEnemy != null)
            {
                // Calculate knockback direction
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

                // Apply damage and knockback to the enemy
                eEnemy.takeDamage(dmg, knockbackDirection, knockbackForce);
            }

            // Instantiate the explosion effect at the boomerang's position
            Quaternion finalRotation = transform.rotation * Quaternion.Euler(0f, 90f, 0f);
            Instantiate(explo, transform.position, finalRotation);
        }
    }

    void Update()
    {
        // Calculate the distance traveled by the boomerang
        float distance = Vector3.Distance(startLocation, transform.position);

        if (distance > slowdownDistance)
        {
            // Optional: Slow down the boomerang after it reaches a certain distance
            // speed *= 0.9f;
        }

        if (distance > range)
        {
            startReturn = true;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero; // Stop forward movement
        }

        if (startReturn)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            if (Vector3.Distance(player.transform.position, transform.position) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
}
