using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stunMan : MonoBehaviour
{
    private GameObject player;
    public GameObject stun;
    public float speed = 5f;
    public float stunRange = 10f;
    public float attackRange = 2f;
    public float attackSpeed = 0.5f;
    public float attackTime = 6f;
    public int damage = 10;
    public float runAwayDistanceDivisor = 2f;
    public float runAwaySpeedDivisor = 2f;
    private float elapsed = 0f;

    public bool hitTarget { get; set; } = false;

    public float shootSpeed = 1f;

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("character");
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Set the NavMeshAgent speed to match the original speed variable
        navMeshAgent.speed = speed;

        // Ensure NavMeshAgent only updates the x and y axis in 2D
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    void Attacking()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < attackRange)
        {
            if (elapsed >= attackSpeed)
            {
                animator.SetBool("isAttacking", true);
                elapsed = 0f;
                characterStats cStats = player.GetComponent<characterStats>();
                cStats.takeDamage(damage);
            }
        }
        if(navMeshAgent.enabled)
        {
            if (Vector3.Distance(player.transform.position, transform.position) > attackRange - 1)
            {
                navMeshAgent.SetDestination(player.transform.position);
            }
            else
            {
                navMeshAgent.ResetPath(); // Stop moving if within attack range
            }
        }        
    }
    
    void stopAttack()
    {
        animator.SetBool("isAttacking", false);
    }
    void Shoot()
    {
        animator.SetBool("isAttacking", true);
        if (elapsed >= shootSpeed)
        {
            elapsed = 0f;
            GameObject instantiatedStun = Instantiate(stun, transform.position, Quaternion.identity);

            // Attach a script or modify properties on the instantiated object if needed
            stun stunScript = instantiatedStun.GetComponent<stun>();
            stunScript.SetCreator(gameObject);
        }
    }

    void Stunning()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer > stunRange)
        {
            animator.SetBool("isWalking", true);
            navMeshAgent.SetDestination(player.transform.position);
        }
        else
        {
            Shoot();

            if (distanceToPlayer < stunRange / runAwayDistanceDivisor)
            {
                Vector3 directionAwayFromPlayer = (transform.position - player.transform.position).normalized;
                Vector3 newDestination = transform.position + directionAwayFromPlayer * (stunRange / runAwayDistanceDivisor);
                navMeshAgent.SetDestination(newDestination);
            }
            else
            {
                navMeshAgent.ResetPath(); // Stop moving if within the stun range
            }
        }
    }

    void CancelAttack()
    {
        hitTarget = false;
    }

    public void StartAttackTimer()
    {
        Invoke("CancelAttack", attackTime);
    }

    void Update()
    {
        if (hitTarget)
        {
            Attacking();
        }
        else
        {
            Stunning();
        }

        elapsed += Time.deltaTime;

        // Keep the Z position fixed to prevent rendering issues
        Vector3 fixedPosition = transform.position;
        fixedPosition.z = 0f;
        transform.position = fixedPosition;
    }
}
