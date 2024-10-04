using System.Collections;
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

    private float speedBuff;

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    public bool getDashStatus()
    {
        return canDash;
    }

    public float getSpeed()
    {
        return moveSpeed;
    }

    public void setSpeed(float speed)
    {
        moveSpeed = speed;
    }

    void Update()
    {
        characterStats cStats = this.gameObject.GetComponent<characterStats>();
        speedBuff = cStats.movementBuff;


        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Set walking animation based on movement
        animator.SetBool("isMoving", movement.magnitude > 0);

        elapsed += Time.deltaTime;

        if (!isDashing && elapsed >= dashResetDuration)
        {
            canDash = true;
        }

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

        if (isDashing)
        {
            if (elapsed >= dashDuration)
            {
                animator.SetBool("isDashing", false);
                isDashing = false;
                elapsed = 0f;
            }

            rb.velocity = movement.normalized * (moveSpeed + speedBuff) * dashIntensity;
        }
        else
        {
            rb.velocity = movement.normalized * (moveSpeed + speedBuff);
        }
    }
}
