using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float jumpForce = 5f;             // The force applied to make the enemy jump
    public float groundCheckDistance = 0.2f; // Distance from the center of the enemy to check for the ground
    public LayerMask groundLayer;            // The layer that represents the ground

    private Rigidbody2D rb;
    private bool isJumping = false;           // Flag to track if the enemy is currently jumping
    private bool isGrounded = false;          // Flag to track if the enemy is currently on the ground

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isGrounded && !isJumping)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        // Cast a ray downwards to check if the enemy is touching the ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        // If the ray hits the ground, the enemy is considered grounded
        isGrounded = hit.collider != null;
    }

    private void Jump()
    {
        isJumping = true;

        // Apply an upward force to make the enemy jump
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}

