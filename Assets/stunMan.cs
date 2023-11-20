using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stunMan : MonoBehaviour
{
    private GameObject player;
    public GameObject stun;
    public float speed = 5f;
    public float stunRange = 10f;
    public float attackRange = 2f;
    public float attackSpeed = 0.5f;
    public float attackTime = 6f;
    public int damage = 10;
    public float runAwayDistanceDivisor = 2f;
    public float runAwaySpeedDivisor = 2f;
    private float elasped = 0f;

    public bool hitTarget{get; set;} = false;

    public float shootSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("character");
    }
    void attacking(){
       
        if(Vector3.Distance(player.transform.position, transform.position) < attackRange){
            if(elasped >= attackSpeed){
                elasped = 0f;
                characterStats cStats = player.GetComponent<characterStats>();
                cStats.takeDamage(damage);
            }
            
        }
        if(Vector3.Distance(player.transform.position, transform.position) > attackRange - 1){
             transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
    void shoot(){
        if(elasped >= shootSpeed){
            elasped = 0f;


            GameObject instantiatedStun = Instantiate(stun, transform.position, Quaternion.identity);

            // Attach a script or modify properties on the instantiated object if needed
            stun stunScript = instantiatedStun.GetComponent<stun>();
            stunScript.SetCreator(gameObject); // Assuming you have a method to set the creator in StunScript
            
        }
    }
    void stunning(){
        if(Vector3.Distance(player.transform.position, transform.position) > stunRange){
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }else{ 
            shoot();       
            if(Vector3.Distance(player.transform.position, transform.position) < stunRange/runAwayDistanceDivisor){
                Vector3 directionToPlayer = transform.position - player.transform.position;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + directionToPlayer, (speed/runAwaySpeedDivisor) * Time.deltaTime);
            }
        }
    }
    void cancelAttack(){
        hitTarget = false;
    }
    public void startAttackTimer(){
        Invoke("cancelAttack", attackTime);
    }
    // Update is called once per frame
    void Update()
    {
        if(hitTarget == true){
            attacking();
        }else{
             stunning();
        }
        elasped += Time.deltaTime;
        
        
    }
}
