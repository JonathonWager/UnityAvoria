using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 7f;
    public GameObject player;
    public int dmg;
    // int dmg = enemyStats eStats;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;

        player = GameObject.FindGameObjectWithTag("character");

        Destroy(gameObject, 2f);

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "character") {
            characterStats cStats = player.GetComponent<characterStats>();
             // enemyStats eStats = this.gameObject.GetComponent<enemyStats>();

            decimal d = dmg;
            cStats.takeDamage(d);

            Destroy(this.gameObject);

        }
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
