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

    private Vector3 startLocation;

    void Start()
    {
        startLocation = transform.position;
        dmg = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().adjAtk;
        range = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().range;
        player = GameObject.FindGameObjectWithTag("character");
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
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
