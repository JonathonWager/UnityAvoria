using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GirxerEnemy : MonoBehaviour
{
    public float roamDistance = 10f;
    public float attackRange = 15f;
    public float damageRange = 2f;
    public float damageCooldown = 3f;
    public int damageAmount = 10;

    private NavMeshAgent navMeshAgent;
    private GameObject player;
    private bool isAttacking = false;
    private Vector3 originalPosition;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("character");
        originalPosition = transform.position;

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        StartCoroutine(Roam());
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer <= damageRange && !isAttacking)
        {
            // Stop roaming if attacking
            StopCoroutine(Roam());
            StartCoroutine(AttackPlayer());
        }
        else if (distanceToPlayer <= attackRange && !isAttacking)
        {
            // Stop roaming and chase the player
            StopCoroutine(Roam());
            navMeshAgent.SetDestination(player.transform.position);
        }
        else if (distanceToPlayer > attackRange && !isAttacking && !IsInvoking(nameof(StartRoam)))
        {
            // Resume roaming if the player is out of range and not attacking
            Invoke(nameof(StartRoam), damageCooldown);
        }

    }

    void StartRoam()
    {
        StartCoroutine(Roam());
    }

    IEnumerator Roam()
    {
        while (true)
        {
            Vector3 randomDirection = Random.insideUnitCircle * roamDistance;
            Vector3 targetPosition = originalPosition + new Vector3(randomDirection.x, randomDirection.y, 0);

            NavMeshHit hit;
            NavMesh.SamplePosition(targetPosition, out hit, roamDistance, NavMesh.AllAreas);

            navMeshAgent.SetDestination(hit.position);

            while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            {
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        navMeshAgent.isStopped = true;

        animator.SetBool("isAttacking", true);
        characterStats cStats = player.GetComponent<characterStats>();
        if (cStats != null)
        {
            cStats.takeDamage(damageAmount);
        }

        yield return new WaitForSeconds(damageCooldown);

        animator.SetBool("isAttacking", false);
        isAttacking = false;
        navMeshAgent.isStopped = false;
    }

}
