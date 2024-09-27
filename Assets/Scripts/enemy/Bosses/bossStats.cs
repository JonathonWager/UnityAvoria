using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossStats : MonoBehaviour
{
    public int hp = 1000;
    public int maxHPDontSet;
    private int halfhp;
    private int quarterhp;
    public string bossType;
    private bool canBeDamaged = true;
      private Animator animator;
          private UnityEngine.AI.NavMeshAgent agent;
    public void takeDamage(int damage)
    {
        Debug.Log("nO famge");
        // Apply damage after considering defense
        if(canBeDamaged){
            hp = hp - damage;
             Debug.Log("big hit");
        }

    }
     public void takeDamage(float damage, Vector2 knockbackDirection, float knockbackForce)
    {
        if(canBeDamaged){
        // cStats.totalDamage += (int)damage;
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttacking", false);
            
            hp -= (int)damage;
            // Apply knockback
            if (agent != null)
            {
                // Disable the NavMeshAgent temporarily
                agent.enabled = false;

                // Apply knockback force manually
                Vector3 knockbackVelocity = new Vector3(knockbackDirection.x, 0, knockbackDirection.y) * knockbackForce;

                // Move the enemy by modifying its position manually for knockback
                StartCoroutine(ApplyKnockback(agent, knockbackVelocity, 0.2f)); // 0.2f is the knockback duration
            }
            if (hp<= 0)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isHurt", false);
                animator.SetBool("isDead", true);
            }
            else
            {
                
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isHurt", true);                
            }
        }
    }
    private IEnumerator ApplyKnockback(UnityEngine.AI.NavMeshAgent agent, Vector3 velocity, float duration)
{
    float timer = 0f;
    while (timer < duration)
    {
        transform.position += velocity * Time.deltaTime;
        timer += Time.deltaTime;
        yield return null;
    }

    // Re-enable the NavMeshAgent after knockback
    agent.enabled = true;
}
    void beegBossUpdate(){
        beegBoss bStats = gameObject.GetComponent<beegBoss>();
        if(hp <= halfhp){
            bStats.pastHalf = true;
        }
        if(hp <= quarterhp){
            bStats.pastQuart = true;
        }
        if(bStats.isVulnerable){
            canBeDamaged = true;
        }else{
            canBeDamaged = false;
        }
    }
    void Update(){
        if(canBeDamaged){
            Transform child = transform.Find("isVulnerable");
            child.gameObject.SetActive(false);

        }else{
                   Transform child = transform.Find("isVulnerable");
            child.gameObject.SetActive(true);
        }
        beegBossUpdate();
        if(hp <= 0){
            Destroy(gameObject);
        }
    }
    void Start(){
        animator = GetComponent<Animator>();
        halfhp = hp /2;
        quarterhp = hp /4;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        maxHPDontSet = hp;
    }
}
