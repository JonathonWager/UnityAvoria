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
    public float accuracy = 0.5f; // The inaccuracy level (higher values mean more inaccuracy)
    private float elapsed = 0f;
    public float attackTime = 5f;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
  

    void Start()
    {
        animator = GetComponent<Animator>();
        elapsed = attackTime;
        player = GameObject.FindGameObjectWithTag("character");
        navMeshAgent = GetComponent<NavMeshAgent>();

        range = Random.Range(range - 1f , range + 1f);
        // Set the speed of the NavMeshAgent to match the original speed
        navMeshAgent.speed = Random.Range(speed - 1f , speed + 1f);

        attackTime = Random.Range(attackTime - 1f, attackTime + 1f);
        minDistance = Random.Range(minDistance - 1f, minDistance + 1f);

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
            if (navMeshAgent != null && navMeshAgent.enabled)
            {
                navMeshAgent.SetDestination(newDestination);
            }
        }
        else if (distanceToPlayer > range)
        {
            animator.SetBool("isWalking", true);
            // Move closer to the player if outside of attack range
            if (navMeshAgent != null && navMeshAgent.enabled)
            {
             navMeshAgent.SetDestination(player.transform.position);
            }
        }
        else
        {
            // Stop moving when the enemy is within the desired distance range
            animator.SetBool("isWalking", false);
            if (navMeshAgent != null && navMeshAgent.enabled)
            {
            navMeshAgent.ResetPath();
            }
        }

        // Handle attacking the player when within range
        if (distanceToPlayer <= range && elapsed >= attackTime)
        {
            animator.SetBool("isAttacking", true);

            // Calculate an inaccurate position around the player's position
            Vector3 inaccuratePosition = player.transform.position + new Vector3(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy), 0);

            // Instantiate the fire at the inaccurate position
            Instantiate(fire, inaccuratePosition, Quaternion.identity);

            elapsed = 0f;
        }

        elapsed += Time.deltaTime;




    }


}
