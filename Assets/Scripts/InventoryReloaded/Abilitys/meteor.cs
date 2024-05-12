using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteor : MonoBehaviour
{
    public float dmgToEnemys = 10;
    public float destroyTime = 0.1f;

     private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "enemy"){
            enemyStats eEnemy = other.gameObject.GetComponent<enemyStats>();
            eEnemy.takeDamage(dmgToEnemys);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        abilityDirector aStats = GameObject.FindGameObjectWithTag("character").GetComponentInChildren<abilityDirector>();;
        dmgToEnemys = dmgToEnemys + aStats.meteorLevel/10f;
        destroyTime = destroyTime + aStats.meteorLevel/20f;
        Destroy(this.gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
