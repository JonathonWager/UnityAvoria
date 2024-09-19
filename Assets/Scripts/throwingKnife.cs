// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class throwingKnife : MonoBehaviour
// {
   
//     private Vector3 targetPosition;

//     // Starting position of the arrow
  

//     public GameObject explo;
//     private Vector3 direction;
//     private Vector3 startLocation;
//     private int dmg;
//     private float range;

//     public float speed = 7f;
//     public float knockbackForce = 5f;

//     void Start()
//     {
//         startLocation = transform.position;
//         targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//         targetPosition.z = transform.position.z;
//         direction = (targetPosition - transform.position).normalized;

//         dmg = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().adjAtk;
//         range = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().range;
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.gameObject.tag == "enemy")
//         {
//             enemyStats eEnemy = other.gameObject.GetComponent<enemyStats>();
//             if (eEnemy != null)
//             {
//                 // Calculate knockback direction
//                 Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

//                 // Apply damage and knockback
//                 eEnemy.takeDamage(dmg, knockbackDirection, knockbackForce);

//                 // Instantiate the explosion effect
//                 Quaternion finalRotation = transform.rotation * Quaternion.Euler(0f, 90f, 0f);
//                 Instantiate(explo, transform.position, finalRotation);
//             }
//         }
//     }

//     void Update()
//     {
//         // Rotate the knife for a spinning effect
//         transform.Rotate(Vector3.forward * 1000f * Time.deltaTime);

//         // Check the distance and destroy if out of range
//         float distance = Vector3.Distance(startLocation, transform.position);
//         if (distance > range)
//         {
//             Destroy(gameObject);
//         }

//         // Move the knife forward
//         transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
//     }
// }
