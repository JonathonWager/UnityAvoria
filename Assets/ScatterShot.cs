using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterShot : MonoBehaviour
{
    public float speed = 5f; // Speed at which the shot moves forward
    public int damage = 10;

    int hitCount = 0;
    public int collateralCount = 1;
    public GameObject explo;
    void Update()
    {
        // Move the scatter shot forward based on its current rotation
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy") || other.CompareTag("PlayerOnlyEnemy"))
        {
            enemyStats eEnemy = other.GetComponent<enemyStats>();
            if (eEnemy != null)
            {
                // Knockback direction and force
                Vector2 knockbackDirection =  transform.up;
                eEnemy.takeDamage(damage, knockbackDirection, 0f);
                Instantiate(explo, other.transform.position, Quaternion.identity);
            }
            GameObject player = GameObject.FindGameObjectWithTag("character");
          
            hitCount += 1;
            if(hitCount >= collateralCount){
                Instantiate(explo, other.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            
        }else if(other.CompareTag("wall")){
            Instantiate(explo, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
