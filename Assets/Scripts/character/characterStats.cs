using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterStats : MonoBehaviour
{
    public int gold = 100;
    public int hp = 100;
    private int basehp;

    public int dmgBuff = 0;
    public float rangeBuff = 0;

    public float movementBuff = 0;

    public int regenAmount = 1;
    public float regenTime = 2f;
    
    private Animator animator;
    // Method to deduct health points based on received damage
    public void takeDamage(decimal damage)
    {
        animator.SetBool("isDamaged", true);
        // Apply damage after considering defense
        hp = hp - (int)(damage);
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
    public void addGold(int Gold){
        gold += Gold;
    }
    // Update is called once per frame
    void Update()
    {
        // Check if the player's health is zero or below
        if (hp <= 0)
        {
            GameObject sceneReset = GameObject.FindGameObjectWithTag("scenereset");
            sceneReset.GetComponent<SceneReset>().StartReset();
            Destroy(this.gameObject);
        }
    }
}