using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterStats : MonoBehaviour
{
    public int gold = 100;
    // Player's health points
    public int hp = 100;
    private int basehp;

    // Player's defense stat
    public int def = 10;

    // Player's base attack stat
    public int baseAtk = 10;
    public int regenAmount = 1;
    public float regenTime = 2f;

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
    
    private Animator animator;
    // Method to deduct health points based on received damage
    public void takeDamage(decimal damage)
    {
        animator.SetBool("isDamaged", true);
        // Apply damage after considering defense
        hp = hp - (int)(damage * (1 - (def / 100)));
    }
    void stopDamage(){
        animator.SetBool("isDamaged", false);
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
        basehp = hp;
        // Initialization code can be added here if needed
        animator = GetComponent<Animator>();
               InvokeRepeating("healthRegen", 0f, regenTime);
    }
    void healthRegen(){
        if(hp < basehp){
            hp += regenAmount;
        }
    }
    void stopAttack(){

            animator.SetBool("isAttacking", false);
    }
    // Method to update weapon-related stats
    public void weaponStats(Weapon curWeap)
    {
        // Calculate adjusted attack and set range based on the current weapon
        adjAtk = (int)((decimal)baseAtk + (decimal)curWeap.damage);
        range = curWeap.range;
        attackResetTime = curWeap.shootInterval;
    }

    // Method for melee attack
 void meleeAttack()
{
    animator.SetBool("isAttacking", true);
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    // Determine the direction from the character to the mouse
    Vector3 directionToMouse = mousePosition - transform.position;

    // Check if the mouse is on the left or right side of the character
    bool mouseIsOnRightSide = directionToMouse.x > 0;

    // Check if the character is currently facing right
    bool characterFacingRight = transform.localScale.x > 0;
    if ((mouseIsOnRightSide && !characterFacingRight) || (!mouseIsOnRightSide && characterFacingRight))
    {
        Flip(); // Function to flip the character
    }

    // Normalize the direction to the mouse for angle comparison
    Vector2 attackDirection = directionToMouse.normalized;

    // Set the attack angle (in degrees)
    float attackAngle = currentSelectedWeapon.attackAngle; // Example: 90 degrees in front of the player

    // Get all colliders within the attack range
    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
    foreach (Collider2D collider in hitColliders)
    {
        if (collider.CompareTag("enemy")) // Check if the collider has the correct tag
        {
            // Determine the direction from the player to the target
            Vector3 directionToTarget = (collider.transform.position - transform.position).normalized;

            // Calculate the angle between the attack direction and the target direction
            float angleToTarget = Vector2.Angle(attackDirection, directionToTarget);

            // Check if the target is within the attack angle
            if (angleToTarget <= attackAngle / 2f)
            {

                // Optionally, apply knockback
                Vector2 knockbackDirection = directionToTarget;
                float knockbackForce = currentSelectedWeapon.knockBack; // Define this in your weapon class
                collider.GetComponent<enemyStats>().takeDamage((int)(adjAtk), knockbackDirection, knockbackForce);

                // Disable attacking temporarily to allow for cooldown
                canAttack = false;
                Invoke("attackReset", attackResetTime);
            }
        }
    }





// Get the current mouse position in the world
        // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // // Determine the direction from the character to the mouse
        // Vector3 directionToMouse = mousePosition - transform.position;

        // // Check if the mouse is on the left or right side of the character
        // bool mouseIsOnRightSide = directionToMouse.x > 0;

        // // Check if the character is currently facing right
        // bool characterFacingRight = transform.localScale.x > 0;

        // // Run the function if the character is not facing the mouse
        // if ((mouseIsOnRightSide && !characterFacingRight) || (!mouseIsOnRightSide && characterFacingRight))
        // {
        //     Flip(); // Function to flip the character
        // }
        // animator.SetBool("isAttacking", true);
        // // Calculate mouse direction and create a ray
        // Vector2 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        // Ray2D ray = new Ray2D(transform.position, mouseDirection);

        // // Cast a ray and check for hits
        // RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 20f);
        // if (hits.Length > 1)
        // {
        //     foreach (RaycastHit2D hit in hits)
        //     {
        //         if (hit.collider != null && hit.collider.gameObject.tag == "enemy")
        //         {
        //             // Check if the enemy is within attack range
        //             if (hit.distance <= range && canAttack)
        //             {
                       
        //                 // Damage the enemy
        //                 enemyStats eEnemy = hit.collider.gameObject.GetComponent<enemyStats>();
        //                 eEnemy.takeDamage((int)(adjAtk));
        //                 canAttack = false;
        //                 Invoke("attackReset", attackResetTime);
        //             }
        //         }
        //         if (hit.collider != null && hit.collider.gameObject.tag == "boss")
        //         {
        //             Debug.Log("hit boss");
        //             // Check if the enemy is within attack range
        //             if (hit.distance <= range && canAttack)
        //             {
        //                 // Damage the enemy
        //                 bossStats bStats = hit.collider.gameObject.GetComponent<bossStats>();
        //                 bStats.takeDamage((int)(adjAtk));
        //                 canAttack = false;
        //                 Invoke("attackReset", attackResetTime);
        //             }
        //         }
        //     }
        // }
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
    public void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // Invert the X scale to flip the sprite
        transform.localScale = theScale;
    }

    // Method to reset the firing cooldown
    void fireReset()
    {
        canFire = true;
    }
    public void addGold(int Gold){
        gold += Gold;
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
            if(canAttack){
                if (currentSelectedWeapon.isRanged == "M")
                {
                    meleeAttack();
                }
                else if (currentSelectedWeapon.isRanged == "R")
                {
                    rangeAttack();
                }
            }
            
        }
    }
}