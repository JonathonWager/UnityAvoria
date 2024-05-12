using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireRing : MonoBehaviour
{
    // Temporary variable to store the character's adjusted attack for later restoration
    int tempDmg;

    // Damage multiplier applied to the character's attack during the fire ring effect
    public float dmgBuff = 2f;

    // Time interval at which enemies within the fire ring take damage
    public float enemyDamageTimeInterval = 2f;

    // Damage inflicted to enemies within the fire ring
    public float dmgToEnemys = 5;

    // Time duration before the fire ring is automatically destroyed
    public float destoryTime = 5f;

    // Flag indicating whether the fire ring is currently damaging enemies
    private bool isDmging = false;
    private int level = 1;

private bool setLevel = false;
    // List to store references to current enemies within the fire ring
    public List<GameObject> currentEnemys = new List<GameObject>();

    // Method to apply damage to enemies within the fire ring
    public void dmgEnemys()
    {
        foreach (GameObject enemy in currentEnemys)
        {
            // Inflict damage to each enemy
            enemyStats eEnemy = enemy.gameObject.GetComponent<enemyStats>();
            eEnemy.takeDamage(dmgToEnemys);
        }
    }
    public void updateLevelModifer(){
        dmgToEnemys = dmgToEnemys + (level/10f);
        dmgBuff = dmgBuff + (level/10f);
    }
    // Called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering object is the character
        if (other.gameObject.tag == "character")
        {
            // Adjust the character's attack based on the damage buff

            characterStats cStats = other.gameObject.GetComponent<characterStats>();
            if(!setLevel){
                abilityDirector aStats = other.gameObject.GetComponentInChildren<abilityDirector>();
                level = aStats.fireRingLevel;
                updateLevelModifer();
                setLevel = true;
            }
            
            tempDmg = cStats.adjAtk;
            cStats.setDamage((int)(dmgBuff * tempDmg));
        }

        // Check if the entering object is an enemy
        if (other.gameObject.tag == "enemy")
        {
            // Add the enemy to the list of current enemies
            currentEnemys.Add(other.gameObject);

            // If not currently damaging, start the damage loop
            if (!isDmging)
            {
                isDmging = true;
                InvokeRepeating("dmgEnemys", 0f, enemyDamageTimeInterval);
            }
        }
    }

    // Called when another collider exits the trigger collider attached to this object
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the exiting object is the character
        if (other.gameObject.tag == "character")
        {
            // Restore the character's original attack
            characterStats cStats = other.gameObject.GetComponent<characterStats>();
            cStats.setDamage(tempDmg);
        }

        // Check if the exiting object is an enemy
        if (other.gameObject.tag == "enemy")
        {
            Debug.Log("Enemy exit");
            
            // Remove the enemy from the list of current enemies
            currentEnemys.Remove(other.gameObject);

            // If there are no more enemies within the fire ring, stop the damage loop
            if (currentEnemys.Count == 0)
            {
                isDmging = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Destroy the fire ring after a specified time
        Destroy(this.gameObject, destoryTime);
    }

    // Update is called once per frame
    void Update()
    {
        // Additional update logic can be added here if needed
    }
}
