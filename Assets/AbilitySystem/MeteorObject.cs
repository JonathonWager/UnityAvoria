using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;

public class MeteorObject : MonoBehaviour
{
    public float dmgToEnemys = 10f;
    public float knockbackForce = 5f;  // Variable for knockback force
    public float size;

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
        this.transform.localScale = new Vector3(size, size, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy" || other.CompareTag("PlayerOnlyEnemy"))
        {
            enemyStats eEnemy = other.gameObject.GetComponent<enemyStats>();
            if (eEnemy != null)
            {
                // Calculate knockback direction from the meteor's center
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

                // Apply damage and knockback to the enemy using the takeDamage method
                eEnemy.takeDamage(dmgToEnemys, knockbackDirection, knockbackForce);
            }
        }
    }
}
