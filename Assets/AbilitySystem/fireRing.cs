using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireRing : MonoBehaviour
{
    int tempDmg;  
    public int playerDamageBuff = 2;  
    public float enemyDamageTimeInterval = 2f;  
    public float damage = 5f;  


    private bool isDmging = false;  
    private int level = 1;  
    private bool setLevel = false;  

    public List<GameObject> currentEnemys = new List<GameObject>();  

    public void dmgEnemys()
    {
        foreach (GameObject enemy in currentEnemys)
        {
            enemyStats eEnemy = enemy.GetComponent<enemyStats>();
            if (eEnemy != null)
            {
                eEnemy.takeDamage(damage, Vector2.zero, 0f);  // Pass Vector2.zero to indicate no knockback
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("character"))
        {
            characterStats cStats = other.GetComponent<characterStats>();
            if (cStats != null)
            {
                if (!setLevel)
                {
                    setLevel = true;
                }
                cStats.dmgBuff += playerDamageBuff;
              //  cStats.setDamage((int)(dmgBuff * tempDmg));
            }
        }

        if (other.CompareTag("enemy")|| other.CompareTag("PlayerOnlyEnemy"))
        {
            Debug.Log("FOUND ENEMY");
            currentEnemys.Add(other.gameObject);
            if (!isDmging)
            {
                isDmging = true;
                InvokeRepeating(nameof(dmgEnemys), 0f, enemyDamageTimeInterval);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("character"))
        {
            characterStats cStats = other.GetComponent<characterStats>();
            if (cStats != null)
            {
                cStats.dmgBuff -= playerDamageBuff;
            }
        }

        if (other.CompareTag("enemy"))
        {
            currentEnemys.Remove(other.gameObject);
            if (currentEnemys.Count == 0)
            {
                isDmging = false;
                CancelInvoke(nameof(dmgEnemys));
            }
        }
    }

    void Start()
    {
        // No destroy logic here, this should be managed by the AbilityBase script.
    }

    void Update()
    {
        // Additional update logic can be added here if needed
    }
}
