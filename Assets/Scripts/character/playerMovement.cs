using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    
    public float moveSpeed = 5f;
    public float dashIntensity = 2f;
    public float dashResetDuration = 3f;

    public float dashDuration = 0.5f;
    
    private float elapsed = 0f;
    private bool isDashing = false;
    public bool canDash = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool getDashStatus(){
        return canDash;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        elapsed += Time.deltaTime;
        if(!isDashing && elapsed >= dashResetDuration){
            canDash = true;
        }
         if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(!isDashing && elapsed >= dashResetDuration){
                 elapsed = 0f;
                 isDashing = true;
                 canDash = false;
            }    
         }

        if(isDashing){
            if(elapsed >= dashDuration){
                isDashing = false;
                
                elapsed = 0f;
            }
             transform.Translate(movement * moveSpeed*dashIntensity * Time.deltaTime);
         }else{
            transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
         }
       
        
        
}
