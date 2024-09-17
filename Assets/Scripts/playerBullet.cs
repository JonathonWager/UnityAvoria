using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public float speed = 7f;
    public GameObject player;
    public int dmg;
    public float range;
    public float knockbackForce = 5f;
         private Vector3 targetDirection; 
    private Vector3 startLocation;

    void Start()
    {
         Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z; // Set the z to be the same as the GameObject
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calculate the direction towards the target position
        targetDirection = (targetPosition - transform.position).normalized;

        // Rotate towards the target direction
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        startLocation = transform.position;
        player = GameObject.FindGameObjectWithTag("character");
        dmg = player.GetComponent<characterStats>().adjAtk;
        range = player.GetComponent<characterStats>().range;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            enemyStats eEnemy = other.gameObject.GetComponent<enemyStats>();
            if (eEnemy != null)
            {
                // Calculate knockback direction
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

                // Apply damage and knockback
                eEnemy.takeDamage(dmg, knockbackDirection, knockbackForce);
            }
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(startLocation, transform.position);
        if (distance > range)
        {
            Destroy(gameObject);
        }

        // Move the bullet forward
        transform.position += targetDirection * speed * Time.deltaTime;
    }
}
