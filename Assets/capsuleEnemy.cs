using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capsuleEnemy : MonoBehaviour
{
    public bool isAttacking = false;
    public float spinSpeed = 5f;

     float elapsed = 0f;
     public float waitTime = 2f;
     public float attackTime = 3f;
    public GameObject playerPos;
  public float shootingSpeed = 250f;


  public bool gotTarget = false;
  public Vector3 targetLocation;
    // Start is called before the first frame update
    void Start()
    {
      
	     playerPos = GameObject.FindGameObjectWithTag("character");
    }
    
    public void lookatplayer(){
        // playerPos = GameObject.FindGameObjectWithTag("character");
        Quaternion rotationAmount = Quaternion.Euler(0, 0, 90);
        Quaternion rotation = Quaternion.LookRotation (playerPos.transform.position - transform.position , transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z , rotation.w) *rotationAmount ;
    }

    void attack(){

    }
    // Update is called once per frame
    void Update()
    {
        
       elapsed += Time.deltaTime;

        if(elapsed >= waitTime){
            if(attackTime <= 0){
                if(!gotTarget){
                    targetLocation = playerPos.transform.position;
                     
                    gotTarget = true;
                }

                transform.position = Vector3.MoveTowards(transform.position, targetLocation, shootingSpeed * Time.deltaTime);
                if(transform.position == targetLocation){
                    elapsed = 0f;
                    attackTime = 3f;
                    gotTarget = false;
                }
            }else{
                lookatplayer();
            }
            
            attackTime -= Time.deltaTime;
            
        }else{

            transform.Rotate(new Vector3(0, 0, 90 * Time.deltaTime*spinSpeed));
        }
        
    }
}
