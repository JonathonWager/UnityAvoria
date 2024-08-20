using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class brimonnolly : MonoBehaviour
{
    public GameObject player;
    public GameObject fire;
    public float speed;
    public float range;       // The optimal range for attacking
    public float minDistance; // The minimum distance before moving away from the player
    private float elapsed = 0f;
    public float attackTime = 5f;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isFacingRight = true; // Track which direction the enemy is facing

    void Start()
    {
        animator = GetComponent<Animator>();
        elapsed = attackTime;
        player = GameObject.FindGameObjectWithTag("character");
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Set the speed of the NavMeshAgent to match the original speed
        navMeshAgent.speed = speed;

        // Ensure NavMeshAgent only updates x and y axis in 2D
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    void stopAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // If the player is too close, move away
        if (distanceToPlayer < minDistance)
        {
            animator.SetBool("isWalking", true);
            Vector3 directionAwayFromPlayer = (transform.position - player.transform.position).normalized;
            Vector3 newDestination = transform.position + directionAwayFromPlayer * (minDistance - distanceToPlayer);
            navMeshAgent.SetDestination(newDestination);
        }
        else if (distanceToPlayer > range)
        {
            animator.SetBool("isWalking", true);
            // Move closer to the player if outside of attack range
            navMeshAgent.SetDestination(player.transform.position);
        }
        else
        {
            // Stop moving when the enemy is within the desired distance range
            animator.SetBool("isWalking", false);
            navMeshAgent.ResetPath();
        }

        // Handle attacking the player when within range
        if (distanceToPlayer <= range && elapsed >= attackTime)
        {
            animator.SetBool("isAttacking", true);
            Instantiate(fire, player.transform.position, Quaternion.identity);
            elapsed = 0f;
        }

        elapsed += Time.deltaTime;

        // Handle flipping the sprite based on movement direction
        HandleFlip();

        // Keep the Z position fixed to prevent rendering issues
        Vector3 fixedPosition = transform.position;
        fixedPosition.z = -1f;
        transform.position = fixedPosition;
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
