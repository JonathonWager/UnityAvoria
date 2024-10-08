using System.Collections;
using UnityEngine;

public class SpinAttacker : MonoBehaviour
{
    private GameObject player;
    public float speed = 10f;

    public int attackDamage = 20;
    public float attackRange = 3f;
    public float attackReset = 2f; // Time to wait before moving again

    public int spinCount = 3;
    public float spinSpeed = 360f;
    public float spinTime = 1f; // Duration of the spin attack
    private bool isSpinning = false;
    private bool isAttacking = false;
    public float engageRange = 1f; // Distance at which the enemy will attack

    bool isWaiting = false;
  private Animator animator;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    public int getDamage()
    {
        return attackDamage;
    }

    public float getRange()
    {
        return attackRange;
    }

    // Start is called before the first frame update
    void Start()
    {
        attackDamage += this.gameObject.GetComponent<enemyStats>().dmgBuff;
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("character");
        animator = GetComponent<Animator>();
        // Disable all child objects (presumably attack visuals)
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);

        // Disable rotation and axis updates for NavMeshAgent to work in 2D
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    // Coroutine to handle attack and spinning behavior
    IEnumerator SpinAttack()
    {
        animator.SetBool("isWalking", false);
         if (!animator.GetBool("isHurt"))
        { 
            animator.SetBool("isAttacking", true);
        }
        // Stop the NavMeshAgent by setting its destination to its current position
        navMeshAgent.SetDestination(transform.position);
        navMeshAgent.isStopped = true;  // Ensure the agent is stopped

        // Activate attack visuals
        isSpinning = true;
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);

        float timer = 0f;

        // Spin for the duration of spinTime
        while (timer < spinTime)
        {
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null; // Wait until the next frame
        }
        isWaiting = true;
        StopSpinning();
    }

    // Stop the spinning and reset attack
    void StopSpinning()
    {
                animator.SetBool("isAttacking", false);
        isSpinning = false;
        foreach (Transform child in transform){
            child.gameObject.SetActive(false);
        }
        Debug.Log("setting rotation to 0");
        transform.rotation = Quaternion.Euler(0, 0, 0); // Reset rotation

        // Wait for a moment after the spin before moving towards the player again
        StartCoroutine(WaitBeforeMoving());
    }

    // Coroutine to reset the attack after a cooldown period and wait before moving
    IEnumerator WaitBeforeMoving()
    {
        // Wait for the attack reset cooldown period
        yield return new WaitForSeconds(attackReset);

        // After the cooldown, allow movement towards the player again
        isAttacking = false;

        // Re-enable NavMeshAgent movement
        navMeshAgent.isStopped = false;
        isWaiting = false;
    }

    // Update is called once per frame
    void Update()
    {
 

        // Keep the z-position fixed for 2D
        Vector3 position = transform.position;
        position.z = 0;
        transform.position = position;

        if (isAttacking)
        {

            if (isSpinning == false && !isWaiting){
                StartCoroutine(SpinAttack());
            }
                
        }
        else
        {
            // Move towards the player if not in attack range
            if (Vector3.Distance(player.transform.position, transform.position) > engageRange)
            {
                 if (!animator.GetBool("isHurt"))
                {   
                    animator.SetBool("isWalking", true);
                    navMeshAgent.SetDestination(player.transform.position);
                }
            }
            else
            {
                // Engage in attack
                isAttacking = true;
            }
        }
    }
}
