using System.Collections;
using UnityEngine;

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

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("character");
    }
      void stopDamageAnimation(){
        animator.SetBool("isHurt", false);
    }
    public void takeDamage(float damage, Vector2 knockbackDirection, float knockbackForce)
    {
        if (hp - damage <= 0)
        {
            animator.SetBool("isDead", true);
        }
        else
        {
            hp -= (int)damage;
            animator.SetBool("isHurt", true);

            // Apply knockback
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

    void killEnemy()
    {
        SpawnGold();
        Destroy(this.gameObject);
    }

    void Update()
    {
        if (hp <= 0)
        {
            DisableAllOtherScripts();
        }
        if (Vector3.Distance(transform.position, player.transform.position) < AgroRange)
        {
            isAgro = true;
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
