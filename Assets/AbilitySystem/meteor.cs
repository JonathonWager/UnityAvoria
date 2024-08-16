using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;

public class Meteor : MonoBehaviour
{
    public float dmgToEnemys = 10f;

    private MeteorSmashAbility ability; // Reference to the MeteorSmashAbility

    private void Start()
    {
        // Find the AbilityManager and get the current E ability if it's a MeteorSmashAbility
        AbilityManager abilityManager = GameObject.FindObjectOfType<AbilityManager>();

        if (abilityManager != null && abilityManager.currentE is MeteorSmashAbility)
        {
            ability = (MeteorSmashAbility)abilityManager.currentE;
            dmgToEnemys += ability.level / 10f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            enemyStats eEnemy = other.gameObject.GetComponent<enemyStats>();
            if (eEnemy != null)
            {
                eEnemy.takeDamage(dmgToEnemys);
            }
        }
    }
}
