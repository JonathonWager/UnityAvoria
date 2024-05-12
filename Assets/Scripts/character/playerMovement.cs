using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Movement speed of the player
    public float moveSpeed = 5f;

    // Intensity of the dash movement
    public float dashIntensity = 2f;

    // Duration before the player can dash again
    public float dashResetDuration = 3f;

    // Duration of a single dash
    public float dashDuration = 0.5f;

    private float elapsed = 0f;
    private bool isDashing = false;

    // Flag indicating whether the player can dash
    public bool canDash = true;

    // Property indicating whether the player is stunned
    public bool isStunned { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code can be added here if needed
    }

    // Getter method to retrieve the dash status
    public bool getDashStatus()
    {
        return canDash;
    }

    // Getter method to retrieve the player's speed
    public float getSpeed()
    {
        return moveSpeed;
    }

    // Setter method to set the player's speed
    public void setSpeed(float speed)
    {
        moveSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure the player doesn't rotate
        transform.rotation = Quaternion.Euler(Vector3.zero);

        // Get horizontal and vertical input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
       
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        //Debug.Log(movement);   
        // Track elapsed time for dash cooldown
        elapsed += Time.deltaTime;

        // Reset dash availability after the cooldown duration
        if (!isDashing && elapsed >= dashResetDuration)
        {
            canDash = true;
        }

        // Trigger dash on Left Shift press
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isDashing && elapsed >= dashResetDuration)
            {
                elapsed = 0f;
                isDashing = true;
                canDash = false;
            }
        }

        // Handle dash movement
        if (isDashing)
        {
            if (elapsed >= dashDuration)
            {
                // End the dash after the specified duration
                isDashing = false;
                elapsed = 0f;
            }

            // Translate the player during the dash
            transform.Translate(movement * moveSpeed * dashIntensity * Time.deltaTime);
        }
        else
        {
            // Regular movement when not dashing
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
    }
}