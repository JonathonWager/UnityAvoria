using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireRing : MonoBehaviour
{
    int tempDmg;
    public float dmgBuff = 2f;
    public float enemyDamageTimeInterval = 2f;
    public int dmgToEnemys = 5;

    public float destoryTime = 5f;
    private bool isDmging = false;
    public List<GameObject> currentEnemys = new List<GameObject>();

    public void dmgEnemys(){
        foreach(GameObject enemy in currentEnemys){
            enemyStats eEnemy = enemy.gameObject.GetComponent<enemyStats>();
            eEnemy.takeDamage(dmgToEnemys);
        }
    }
     private void OnTriggerEnter2D(Collider2D other)
    {
         Debug.Log("Entered GameObject Tag: " + other.gameObject.tag);
        if(other.gameObject.tag == "character"){
            characterStats cStats = other.gameObject.GetComponent<characterStats>();
            tempDmg = (int)cStats.getadjAtk();
            cStats.setDamage((int)(dmgBuff * tempDmg));
        }
        if(other.gameObject.tag == "enemy"){
            Debug.Log("enemy entered");
            currentEnemys.Add(other.gameObject);
            if(!isDmging){
                isDmging = true;
                InvokeRepeating("dmgEnemys", 0f, enemyDamageTimeInterval);
            }
             
        }
       
    }
    
     private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "character"){
            characterStats cStats = other.gameObject.GetComponent<characterStats>();
            cStats.setDamage(tempDmg);
        }
        if(other.gameObject.tag == "enemy"){
            Debug.Log("enemy exit");
            currentEnemys.Remove(other.gameObject);
            if(currentEnemys.Count == 0){
                isDmging = false;
            }
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
