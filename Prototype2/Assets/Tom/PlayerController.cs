using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 8f;

    [Header("Jump")]
    public float jumpForce = 16f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;

    [Range(0f, 1f)]
    public float jumpCutMultiplier = 0.5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public Vector3 groundCheckSize = new Vector3(0.5f, 0.1f, 0.5f);
    public LayerMask groundLayer;

    private Rigidbody rb;

    private float moveInput;
    private float coyoteCounter;
    private float jumpBufferCounter;

    private bool isGrounded;
    private bool jumpHeld;
    private bool jumpCutRequested;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Keep a 2.5D character on its existing Z plane.
        rb.constraints |= RigidbodyConstraints.FreezePositionZ;
    }

    // PlayerInput: Send Messages or Invoke Unity Events
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().x;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            jumpBufferCounter = jumpBufferTime;
            jumpHeld = true;
        }
        else
        {
            jumpHeld = false;
            jumpCutRequested = true;
        }
    }

    void Update()
    {
        // Count down the jump buffer using frame time.
        jumpBufferCounter = Mathf.Max(
            0f,
            jumpBufferCounter - Time.deltaTime
        );
    }

    void FixedUpdate()
    {
        // Check the ground during the physics step.
        isGrounded = false;

        if (groundCheck != null)
        {
            isGrounded = Physics.CheckBox(
                groundCheck.position,
                groundCheckSize * 0.5f,
                groundCheck.rotation,
                groundLayer,
                QueryTriggerInteraction.Ignore
            );
        }

        // Refresh coyote time while grounded.
        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter = Mathf.Max(
                0f,
                coyoteCounter - Time.fixedDeltaTime
            );
        }

        // Horizontal movement.
        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveInput * moveSpeed;
        rb.linearVelocity = velocity;

        // Cut the jump once when the jump button is released.
        if (jumpCutRequested)
        {
            velocity = rb.linearVelocity;

            if (velocity.y > 0f)
            {
                velocity.y *= jumpCutMultiplier;
                rb.linearVelocity = velocity;
            }

            jumpCutRequested = false;
        }

        // Execute a buffered jump if grounded or within coyote time.
        if (jumpBufferCounter > 0f && coyoteCounter > 0f)
        {
            velocity = rb.linearVelocity;
            velocity.y = jumpForce;
            rb.linearVelocity = velocity;

            jumpBufferCounter = 0f;
            coyoteCounter = 0f;
            isGrounded = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.green;

        Gizmos.matrix = Matrix4x4.TRS(
            groundCheck.position,
            groundCheck.rotation,
            Vector3.one
        );

        Gizmos.DrawWireCube(
            Vector3.zero,
            groundCheckSize
        );
    }
}