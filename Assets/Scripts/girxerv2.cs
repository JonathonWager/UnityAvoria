using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girxerv2 : MonoBehaviour
{
    public float damageRange = 2f;
    public float damageCooldown = 3f;
    public int damageAmount = 10;

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private GameObject player;
    private bool isAttacking = false;
    private Vector3 originalPosition;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("character");
        originalPosition = transform.position;

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

    }

    void Update()
    {
        if(navMeshAgent.enabled == true){
             float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (distanceToPlayer <= damageRange && !isAttacking)
            {
                // Stop roaming if attacking
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", true);
                isAttacking = true;
                navMeshAgent.isStopped = true;

                Invoke("ResetAttack", damageCooldown);
            }else
            {
                animator.SetBool("isWalking", true);
                navMeshAgent.SetDestination(player.transform.position);
            }
        }
       

    }
    void ResetAttack(){
        animator.SetBool("isAttacking", false);
        isAttacking = false;
        navMeshAgent.isStopped = false;
    }
    public void DoDamage(){
        characterStats cStats = player.GetComponent<characterStats>();
        if (cStats != null)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (distanceToPlayer <= damageRange)
            {
                cStats.takeDamage(damageAmount +this.gameObject.GetComponent<enemyStats>().dmgBuff);
            }
        }   
    }
  
}
