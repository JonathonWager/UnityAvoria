using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charger : MonoBehaviour
{
    float elapsed = 0f;
    public float waitTime = 2f;
    public float attackTime = 3f;
    public GameObject playerPos;
    public float shootingSpeed = 1f;
    public int damage = 10;

    public bool gotTarget = false;
    public Vector3 targetLocation;

    private Animator animator;
    private bool isFacingRight = true; // Tracks the facing direction
    private bool isAgro  = false;
    public float chompRange = 2f;
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("character");
        animator = GetComponent<Animator>();
    }
    
   private void OnTriggerEnter2D(Collider2D collision)
{
    if(collision.gameObject.CompareTag("character")) 
    {
        characterStats cStats = collision.GetComponent<characterStats>();
        if (cStats != null)
        {
            cStats.takeDamage(damage);
        }
    }
}

    void Flip()
    {
        // Flip the GameObject by inverting its local scale
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    void stopChomp(){
        animator.SetBool("isAttacking", false);
    }
    void Update()
    {
            transform.rotation = Quaternion.identity;
            // Determine if the enemy needs to flip to face the player
            if (playerPos != null)
            {
                if (transform.position.x < playerPos.transform.position.x && !isFacingRight)
                {
                    isFacingRight = true;
                    Flip();
                }
                else if (transform.position.x > playerPos.transform.position.x && isFacingRight)
                {
                    isFacingRight = false;
                    Flip();
                }
            }
            
            elapsed += Time.deltaTime;

            if(elapsed >= waitTime){
                if(attackTime <= 0){
                    if(!gotTarget){
                        if(transform.position.x > playerPos.transform.position.x && transform.position.y > playerPos.transform.position.y ){
                            targetLocation = playerPos.transform.position + new Vector3(-5f,-5f,0);
                        }
                        if(transform.position.x < playerPos.transform.position.x && transform.position.y > playerPos.transform.position.y ){
                            targetLocation = playerPos.transform.position + new Vector3(5f,-5f,0);
                        }
                        if(transform.position.x > playerPos.transform.position.x && transform.position.y < playerPos.transform.position.y ){
                            targetLocation = playerPos.transform.position + new Vector3(-5f,5f,0);
                        }
                        if(transform.position.x < playerPos.transform.position.x && transform.position.y < playerPos.transform.position.y ){
                            targetLocation = playerPos.transform.position + new Vector3(5f,5f,0);
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
