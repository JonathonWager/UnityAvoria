using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintDuration = 0.5f;
     public float sprintRechargeTime= 1f;
    float elapsed = 0f;

    public bool isSprinting = false;
    public bool canSprint = true;

    public float sprintBoost = 30f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("sprintRecharge", 1f, 1f);
    }
    void sprintRecharge()
    {
        if(!canSprint){
            if(sprintRechargeTime == 5f){
                canSprint = true;
            }
        }
        if(!isSprinting){
            if(sprintRechargeTime < 5f){
                sprintRechargeTime = sprintRechargeTime + 0.5f;
            }else if( sprintRechargeTime > 5f){
                sprintRechargeTime = 5f;
        }
        }
        
        
    }

    public int getSprint(){
        return (int)sprintDuration;
    }
    // Update is called once per frame
    void Update()
    {
       transform.rotation = Quaternion.Euler(0, 0, 0);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Normalize the movement vector to ensure consistent speed in all directions
        //movement.Normalize();

        // Move the game object based on input
        bool isShiftKeyDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
         if(isShiftKeyDown){
           

            elapsed += Time.deltaTime;
            if(elapsed >= sprintRechargeTime){
                transform.Translate(movement * moveSpeed * Time.deltaTime);
                canSprint = false;
                elapsed = 0f;
            }else if(canSprint){
                isSprinting = true;
                if(elapsed >= 1f){
                    sprintRechargeTime = sprintRechargeTime - 1f;
                    elapsed =0f;
                }
                transform.Translate(movement * sprintBoost  * Time.deltaTime);
            }else{
                 transform.Translate(movement * moveSpeed * Time.deltaTime);
            }
            
        }else{
              isSprinting = false;
             transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
        
    }
}
