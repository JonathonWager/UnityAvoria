using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwingKnife : MonoBehaviour
{
    public GameObject explo;
     private Vector3 direction;
    // Target position for the arrow
    private Vector3 targetPosition;

    // Starting position of the arrow
    private Vector3 startLocation;
    // Start is called before the first frame update
    private int dmg;
    private float range;

    public float speed = 7f;

    void Start()
    {
        startLocation = transform.position;
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;
        direction = (targetPosition - transform.position).normalized;


        dmg = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().adjAtk;
        range = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().range;
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
         transform.Rotate(Vector3.forward * 1000f * Time.deltaTime);
        float distance = Vector3.Distance(startLocation, transform.position);
        if(distance > range){
            Destroy(gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
}
