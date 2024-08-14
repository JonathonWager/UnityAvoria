using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterStats : MonoBehaviour
{
    public int gold = 100;
    // Player's health points
    public int hp = 100;

    // Player's defense stat
    public int def = 10;

    // Player's base attack stat
    public int baseAtk = 10;

    // Player's adjusted attack stat (modifiable)
    public int adjAtk { get; set; } = 10;

    // Attack range of the player
    public float range { get; set; } = 2f;

    // Reference to the ranged attack prefab
    public GameObject rangeObject { get; set; }

    // Currently selected weapon
    public Weapon currentSelectedWeapon { get; set; }

    // Flag to control firing cooldown
    private bool canFire = true;
    private bool canAttack = true;
    public float attackResetTime = 0.5f;

    // Method to deduct health points based on received damage
    public void takeDamage(decimal damage)
    {
        // Apply damage after considering defense
        hp = hp - (int)(damage * (1 - (def / 100)));
    }

    // Getter method to retrieve current health points
    public int getHp()
    {
        return hp;
    }
     public void setHp(int newHp)
    {
        hp = newHp;
    }

    // Setter method to modify the adjusted attack stat
    public void setDamage(int dmgBuff)
    {
        adjAtk = dmgBuff;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code can be added here if needed
    }

    // Method to update weapon-related stats
    public void weaponStats(Weapon curWeap)
    {
        // Calculate adjusted attack and set range based on the current weapon
        Debug.Log((decimal)curWeap.range);
        adjAtk = (int)((decimal)baseAtk + (decimal)curWeap.damage);
        range = curWeap.range;
        attackResetTime = curWeap.shootInterval;
    }

    // Method for melee attack
    void melleAttack()
    {
        // Calculate mouse direction and create a ray
        Vector2 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Ray2D ray = new Ray2D(transform.position, mouseDirection);

        // Cast a ray and check for hits
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 20f);
        if (hits.Length > 1)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject.tag == "enemy")
                {
                    // Check if the enemy is within attack range
                    if (hit.distance <= range && canAttack)
                    {
                        // Damage the enemy
                        enemyStats eEnemy = hit.collider.gameObject.GetComponent<enemyStats>();
                        eEnemy.takeDamage((int)(adjAtk));
                        canAttack = false;
                        Invoke("attackReset", attackResetTime);
                    }
                }
                if (hit.collider != null && hit.collider.gameObject.tag == "boss")
                {
                    Debug.Log("hit boss");
                    // Check if the enemy is within attack range
                    if (hit.distance <= range && canAttack)
                    {
                        // Damage the enemy
                        bossStats bStats = hit.collider.gameObject.GetComponent<bossStats>();
                        bStats.takeDamage((int)(adjAtk));
                        canAttack = false;
                        Invoke("attackReset", attackResetTime);
                    }
                }
            }
        }
    }
    void attackReset(){
        canAttack = true;
    }
    // Method for ranged attack
    void rangeAttack()
    {
        // Instantiate a ranged attack object
        if (canFire)
        {
            Instantiate(rangeObject, this.transform.position, Quaternion.identity);
            canFire = false;

            // Set a cooldown for firing
            Invoke("fireReset", currentSelectedWeapon.shootInterval);
        }
    }

    // Method to reset the firing cooldown
    void fireReset()
    {
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player's health is zero or below
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }

        // Check for player input to perform attacks
        if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button
        {
            if (currentSelectedWeapon.isRanged == "M")
            {
                melleAttack();
            }
            else if (currentSelectedWeapon.isRanged == "R")
            {
                rangeAttack();
            }
        }
    }
}