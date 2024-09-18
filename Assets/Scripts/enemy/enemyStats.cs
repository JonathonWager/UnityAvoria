using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class enemyStats : MonoBehaviour
{
    public int hp;
    public float speed;
    public float def;
    public float AgroRange;
    public bool isAgro;
    public int minGold = 0;
    public int maxGold = 2;

    private Animator animator;
    private GameObject player;
    private NavMeshAgent agent;
    private bool dead = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("character");
         agent = GetComponent<NavMeshAgent>();
    }
      void stopDamageAnimation(){
        animator.SetBool("isHurt", false);
    }
    
    public void takeDamage(float damage, Vector2 knockbackDirection, float knockbackForce)
    {
        hp -= (int)damage;
        if (hp<= 0)
        {
            animator.SetBool("isDead", true);
        }
        else
        {

            animator.SetBool("isHurt", true);

            // Apply knockback
            if (agent != null)
            {
                // Disable the NavMeshAgent temporarily
                agent.enabled = false;

                // Apply knockback force manually
                Vector3 knockbackVelocity = new Vector3(knockbackDirection.x, 0, knockbackDirection.y) * knockbackForce;

                // Move the enemy by modifying its position manually for knockback
                StartCoroutine(ApplyKnockback(agent, knockbackVelocity, 0.2f)); // 0.2f is the knockback duration
            }else{
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Apply knockback force to the Rigidbody
                    Vector2 knockbackForce2D = knockbackDirection.normalized * knockbackForce;
                    rb.AddForce(knockbackForce2D, ForceMode2D.Impulse);
                    StartCoroutine(ResetKnockback(rb, 0.2f));
                }
            }
        }
    }
private IEnumerator ResetKnockback(Rigidbody2D rb, float delay)
{
    yield return new WaitForSeconds(delay);

    // Reset velocity to stop sliding
    rb.velocity = Vector2.zero;
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
    void killEnemy()
    {
        SpawnGold();
        Destroy(this.gameObject);
    }

    void Update()
    {
        if (hp <= 0 && !dead)
        {
            dead = true;
            DisableAllOtherScripts();
            if(agent != null){
                agent.enabled = false; 
            }


        }
    }

    void SpawnGold()
    {
        int goldAmount = Random.Range(minGold, maxGold);
        if (goldAmount > 0)
        {
            GameObject goldPrefab = Resources.Load<GameObject>("Gold");
            GameObject goldObject = Instantiate(goldPrefab, transform.position, Quaternion.identity);
            Gold goldScript = goldObject.GetComponent<Gold>();
            goldScript.Initialize(goldAmount);
        }
    }

    void DisableAllOtherScripts()
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
            {
                script.enabled = false;
            }
        }
    }
}
