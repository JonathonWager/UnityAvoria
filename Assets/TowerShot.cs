using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShot : MonoBehaviour
{
    public int damage = 10;
    public float speed = 5;
    private Vector3 moveDirection;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("character");
        if (player != null)
        {
            // Calculate the direction to move towards
            moveDirection = (player.transform.position - transform.position).normalized;

            // Rotate the projectile to face the player
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        
            Destroy(gameObject,4f); // Destroy the projectile if no target is found
        
    }

    void Update()
    {
        // Move the projectile continuously in the set direction
        transform.position += moveDirection * speed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D other) {
         Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("character"))
        {
            other.GetComponent<characterStats>().takeDamage(damage);
        }

        // Destroy the projectile on impact unless it hits another enemy
        if (!other.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }
}
