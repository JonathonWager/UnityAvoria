using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireRing : MonoBehaviour
{
    int tempDmg;  
    public float dmgBuff = 2f;  
    public float enemyDamageTimeInterval = 2f;  
    public float dmgToEnemys = 5f;  

    private bool isDmging = false;  
    private int level = 1;  
    private bool setLevel = false;  

    public List<GameObject> currentEnemys = new List<GameObject>();  

    public void dmgEnemys()
    {
        Debug.Log("Damaging Enemys");
        foreach (GameObject enemy in currentEnemys)
        {
            enemyStats eEnemy = enemy.GetComponent<enemyStats>();
            if (eEnemy != null)
            {
                eEnemy.takeDamage(dmgToEnemys, Vector2.zero, 0f);  // Pass Vector2.zero to indicate no knockback
            }
        }
    }

    public void updateLevelModifier()
    {
        dmgToEnemys += level / 10f;
        dmgBuff += level / 10f;
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
                tempDmg = cStats.adjAtk;
                cStats.setDamage((int)(dmgBuff * tempDmg));
            }
        }

        if (other.CompareTag("enemy"))
        {
            Debug.Log("adding enemy");
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
                cStats.setDamage(tempDmg);
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
