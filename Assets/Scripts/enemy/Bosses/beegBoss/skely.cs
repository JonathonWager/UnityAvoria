using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skely : MonoBehaviour
{
    [SerializeField] Transform target;
    UnityEngine.AI.NavMeshAgent agent;

    GameObject player;

    public int dmg = 5;
    public float range = 0.4f;

    public float attackSpeed = 0.5f;
    float elapsed = 0f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        transform.rotation = Quaternion.Euler(Vector3.zero);
        target = GameObject.FindGameObjectWithTag("character").transform;
        player = GameObject.FindGameObjectWithTag("character");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if(Vector3.Distance(target.position, transform.position) <= range){
            animator.SetBool("isAttacking", true);
            if(elapsed >= attackSpeed){
                elapsed = 0f;
                characterStats cStats = player.GetComponent<characterStats>();
                cStats.takeDamage(dmg);
            }
          
        }else{
             if (!animator.GetBool("isHurt"))
            { 
                animator.SetBool("isWalking", true);
            }
             agent.SetDestination(target.position);
        }
       
    }
}
