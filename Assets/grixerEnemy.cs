using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class girxerEnemy : MonoBehaviour
{
    public float roamDistance = 10f;   // Distance to roam from the current position
    public float attackRange = 15f;    // Range at which the enemy will chase the player
    public float damageRange = 2f;     // Range at which the enemy will damage the player
    public float damageCooldown = 3f;  // Cooldown between attacks
    public int damageAmount = 10;      // Amount of damage to deal to the player

    private NavMeshAgent navMeshAgent;
    private GameObject player;
    private bool isAttacking = false;
    private bool isRoaming = true;
    private Vector3 originalPosition;
    private Animator animator;

    private bool isFacingRight = true; // Track which direction the enemy is facing

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("character");
        originalPosition = transform.position;

        // Ensure NavMeshAgent only updates x and y axis in 2D
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        StartCoroutine(Roam());
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= attackRange && !isAttacking)
        {
            isRoaming = false;
            StopCoroutine(Roam());

            // Continuously update the destination to follow the player
            navMeshAgent.SetDestination(player.transform.position);
        }
        else if (distanceToPlayer > attackRange && !isRoaming)
        {
            isRoaming = true;
            StartCoroutine(Roam());
        }

        if (!isAttacking && distanceToPlayer <= damageRange)
        {
            StartCoroutine(AttackPlayer());
        }

        // Handle flipping the sprite based on movement direction
        HandleFlip();

        // Keep the z position fixed to -1 to prevent rendering issues
        Vector3 fixedPosition = transform.position;
        fixedPosition.z = -1f;
        transform.position = fixedPosition;
    }

    IEnumerator Roam()
    {
        animator.SetBool("isWalking", true);

        while (true)
        {
            Vector3 randomDirection = Random.insideUnitCircle * roamDistance;
            Vector3 targetPosition = originalPosition + new Vector3(randomDirection.x, randomDirection.y, 0);

            NavMeshHit hit;
            NavMesh.SamplePosition(targetPosition, out hit, roamDistance, NavMesh.AllAreas);

            navMeshAgent.SetDestination(hit.position);

            // Wait until the agent reaches the destination
            while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            {
                yield return null; // Wait until the next frame
            }

            // Pause before picking a new destination
            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }

    IEnumerator AttackPlayer()
    {
        animator.SetBool("isAttacking", true);
        isAttacking = true;
        navMeshAgent.isStopped = true;

        // Damage player
        characterStats cStats = player.GetComponent<characterStats>();
        if (cStats != null)
        {
            cStats.takeDamage(damageAmount);
        }

        // Wait for cooldown
        yield return new WaitForSeconds(damageCooldown);
        animator.SetBool("isAttacking", false);

        isAttacking = false;
        navMeshAgent.isStopped = false;

        // Continue following the player after the attack
        if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
        else
        {
            isRoaming = true;
            StartCoroutine(Roam());
        }
    }

    private void HandleFlip()
    {
        if (navMeshAgent.velocity.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (navMeshAgent.velocity.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
