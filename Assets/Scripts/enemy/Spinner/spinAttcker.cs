using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinAttcker : MonoBehaviour
{
    private GameObject player;
    public float speed = 10f;

    public int attackDamage = 20;
    public float attackRange = 3f;
    public float attackReset = 2f;

    public int spinCount = 3;
    public float spinSpeed = 360f;
    public float spinTime = 1f;
    private bool isSpinning = false;
    private bool isAttacking = false;
    public float engageRange = 1f;

     private UnityEngine.AI.NavMeshAgent navMeshAgent;
    public int getDamage(){
        return attackDamage;
    }
    public float getRange(){
        return attackRange;
    }
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("character");
        foreach (Transform child in transform)
                child.gameObject.SetActive(false);

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }
    void stopAttacking(){
        isAttacking = false;
    }
    void stopSpinning(){
        isSpinning = false;
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Invoke("stopAttacking", attackReset);
    }
    void attack(){
        
         if (isSpinning)
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(true);
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
            Invoke("stopSpinning", spinTime);
            
        }
        
    }
    // Update is called once per frame
    void Update()
    {
         Vector3 position = transform.position;
        position.z = 0;
        transform.position = position;
        if(isAttacking){
            attack();
        }else{
            if(Vector3.Distance(player.transform.position, transform.position) > engageRange){
                navMeshAgent.SetDestination(player.transform.position);
            }else{
                isAttacking = true;
                isSpinning = true;
            }
        }
        
        
    }
}
