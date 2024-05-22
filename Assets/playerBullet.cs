using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{

    public float speed = 7f;
    public GameObject player;
    public int dmg;
    public float range ;
    // int dmg = enemyStats eStats;
  private Vector3 startLocation;
    // Start is called before the first frame update
    void Start()
    {
         startLocation = transform.position;
        dmg = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().adjAtk;
        range = GameObject.FindGameObjectWithTag("character").GetComponent<characterStats>().range;
        player = GameObject.FindGameObjectWithTag("character");


        
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
            //Quaternion finalRotation = transform.rotation * Quaternion.Euler(0f, 90f, 0f);
            //Instantiate(explo, transform.position, finalRotation);
        }

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(startLocation, transform.position);
        if(distance > range){
            Destroy(gameObject);
        }
         transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
