using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireRing : MonoBehaviour
{
    int tempDmg;
    public float dmgBuff = 2f;
    public float enemyDamageTimeInterval = 2f;
    public int dmgToEnemys = 5;
    float elapsed = 0f;

    public float destoryTime = 5f;
     private void OnTriggerEnter2D(Collider2D other)
    {
       
        if(other.gameObject.tag == "character"){
            characterStats cStats = other.gameObject.GetComponent<characterStats>();
            tempDmg = (int)cStats.getadjAtk();
            cStats.setDamage((int)(dmgBuff * tempDmg));
        }
       
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        elapsed += Time.deltaTime;
         
        if(other.gameObject.tag == "enemy"){
            Debug.Log("entered");//PROBLEM
            if(elapsed >= enemyDamageTimeInterval)
                elapsed = 0f;
                enemyStats eEnemy = other.gameObject.GetComponent<enemyStats>();
                eEnemy.takeDamage(dmgToEnemys);
            }
            
            
    }
    
     private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "character"){
            characterStats cStats = other.gameObject.GetComponent<characterStats>();
            cStats.setDamage(tempDmg);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, destoryTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
