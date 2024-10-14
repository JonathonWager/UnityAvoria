using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charger : MonoBehaviour
{
    float elapsed = 0f;
    public float waitTime = 2f;
    public float attackTime = 3f;

    public float shootingSpeed = 1f;
    public int damage = 10;

    public bool gotTarget = false;
    public Vector3 targetLocation;

    private Animator animator;
    private bool isFacingRight = true; // Tracks the facing direction

    public float chompRange = 2f;
    GameObject playerPos;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("character");
    }
    
   private void OnTriggerEnter2D(Collider2D collision)
{
    if(collision.gameObject.CompareTag("character")) 
    {
        characterStats cStats = collision.GetComponent<characterStats>();
        if (cStats != null && this.enabled == true)
        {
            cStats.takeDamage(damage + this.gameObject.GetComponent<enemyStats>().dmgBuff);
        }
    }
}

  
    void stopChomp(){
        animator.SetBool("isAttacking", false);
    }
    void Update()
    {
            
        elapsed += Time.deltaTime;

        if(elapsed >= waitTime){
            if(attackTime <= 0){
                if(!gotTarget){
                    if(transform.position.x > playerPos.transform.position.x && transform.position.y > playerPos.transform.position.y ){
                        targetLocation = playerPos.transform.position + new Vector3(-4f,-4f,0);
                    }
                    if(transform.position.x < playerPos.transform.position.x && transform.position.y > playerPos.transform.position.y ){
                        targetLocation = playerPos.transform.position + new Vector3(4f,-4f,0);
                    }
                    if(transform.position.x > playerPos.transform.position.x && transform.position.y < playerPos.transform.position.y ){
                        targetLocation = playerPos.transform.position + new Vector3(-4f,4f,0);
                    }
                    if(transform.position.x < playerPos.transform.position.x && transform.position.y < playerPos.transform.position.y ){
                        targetLocation = playerPos.transform.position + new Vector3(4f,4f,0);
                    }             
                    gotTarget = true;
                }
                animator.SetBool("isMoving", true);
                transform.position = Vector3.MoveTowards(transform.position, targetLocation, shootingSpeed * Time.deltaTime);
                
                if(transform.position == targetLocation){
                    animator.SetBool("isMoving", false);
                    elapsed = 0f;
                    attackTime = (float)Random.Range(0, 4);
                    gotTarget = false;
                }else{
                    if(Vector3.Distance(transform.position, playerPos.transform.position) < chompRange){
                        animator.SetBool("isAttacking", true);
                    }
                }
            }
            attackTime -= Time.deltaTime;
        }
    }

}
