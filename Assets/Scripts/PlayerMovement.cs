using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (rb == null)
        {
            Debug.LogError("Player needs a Rigidbody2D component!");
        }

        if (spriteRenderer == null)
        {
            Debug.LogWarning("Player needs a SpriteRenderer component to flip direction.");
        }
    }

    void Update()
    {
        // Get horizontal movement input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Face the direction of movement
        if (spriteRenderer != null && moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }

        // Check whether the player is touching the ground
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(
                groundCheck.position,
                groundCheckRadius,
                groundLayer
            );
        }
        else
        {
            isGrounded = false;
        }

        // Jump with Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed. Is Grounded: " + isGrounded);

            if (isGrounded && rb != null)
            {
                Debug.Log("Space pressed - testing jump!");

                rb.linearVelocity = new Vector2(
                    rb.linearVelocity.x,
                    jumpForce
                );
            }
        }

        // Better Jump: release Space to reduce upward movement
        if (Input.GetKeyUp(KeyCode.Space) &&
            rb != null &&
            rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                rb.linearVelocity.y * 0.5f
            );
        }
    }

    void FixedUpdate()
    {
        // Move the player left and right
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(
                moveInput * moveSpeed,
                rb.linearVelocity.y
            );
        }
    }

    void OnDrawGizmosSelected()
    {
        // Display the ground-check area in the Scene view
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(
                groundCheck.position,
                groundCheckRadius
            );
        }
    }
}
