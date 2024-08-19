using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private bool facingRight = true;

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
    private Animator animator;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code can be added here if needed
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }
      public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // Invert the X scale to flip the sprite
        transform.localScale = theScale;
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
           // Example usage: flip when pressing the left or right arrow keys
        if (Input.GetKeyDown(KeyCode.A) && facingRight)
        {
            Flip();
        }
        else if (Input.GetKeyDown(KeyCode.D) && !facingRight)
        {
            Flip();
        }
        if (transform.position != lastPosition)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            
            animator.SetBool("isMoving", false);
        }

        // Update lastPosition to the current position for the next frame
        lastPosition = transform.position;
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
                animator.SetBool("isDashing", true);
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
                animator.SetBool("isDashing", false);
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