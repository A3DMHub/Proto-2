using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Ground")]
    public LayerMask groundLayer;

    [Header("Sprites & Colliders")]
    public SpriteRenderer spriteRenderer;
    public Sprite standSprite;
    public Sprite crouchSprite;

    [Header("Crouch Settings")]
    public float standingColliderHeight = 0.8f;
    public float standingColliderCenterY = 0.05f;
    public float crouchingColliderHeight = 0.4f;
    public float crouchingColliderCenterY = -0.15f;

    private Rigidbody rb;
    private BoxCollider boxCollider;
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool isCrouching = false;

    private readonly HashSet<Collider> groundContacts = new HashSet<Collider>();
    private readonly Dictionary<Collider, Vector3> wallNormals = new Dictionary<Collider, Vector3>();

    private bool IsGrounded => groundContacts.Count > 0;
    public bool IsCrouching => isCrouching;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionZ
                       | RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationY
                       | RigidbodyConstraints.FreezeRotationZ;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        UpdateCrouchState(false);
    }

    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>();
    public void OnJump(InputValue value) => jumpPressed = value.isPressed;

    private void Update()
    {
        // Continuously check if the 'C' key is held down or released
        bool crouchKeyHeld = Keyboard.current != null && Keyboard.current.cKey.isPressed;

        if (crouchKeyHeld != isCrouching)
        {
            UpdateCrouchState(crouchKeyHeld);
        }
    }

    private void UpdateCrouchState(bool crouching)
    {
        isCrouching = crouching;

        if (spriteRenderer != null)
        {
            if (isCrouching && crouchSprite != null)
            {
                spriteRenderer.sprite = crouchSprite;
            }
            else if (!isCrouching && standSprite != null)
            {
                spriteRenderer.sprite = standSprite;
            }
        }

        if (boxCollider != null)
        {
            Vector3 size = boxCollider.size;
            Vector3 center = boxCollider.center;

            if (isCrouching)
            {
                size.y = crouchingColliderHeight;
                center.y = crouchingColliderCenterY;
            }
            else
            {
                size.y = standingColliderHeight;
                center.y = standingColliderCenterY;
            }

            boxCollider.size = size;
            boxCollider.center = center;
        }
    }

    private void FixedUpdate()
    {
        bool grounded = IsGrounded;

        float xVel = -moveInput.x * moveSpeed;
        float yVel = rb.linearVelocity.y;

        if (-moveInput.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (-moveInput.x > 0)
        {
            spriteRenderer.flipX = true;
        }

        if (grounded && yVel <= 0f)
            yVel = 0f;

        foreach (var n in wallNormals.Values)
        {
            if (xVel * n.x < 0f)
            {
                xVel = 0f;
                break;
            }
        }

        rb.linearVelocity = new Vector3(xVel, yVel, 0f);

        if (jumpPressed && grounded)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        jumpPressed = false;
    }

    private void OnCollisionEnter(Collision c) => ProcessContacts(c);
    private void OnCollisionStay(Collision c) => ProcessContacts(c);

    private void OnCollisionExit(Collision c)
    {
        groundContacts.Remove(c.collider);
        wallNormals.Remove(c.collider);
    }

    private void ProcessContacts(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) == 0)
        {
            groundContacts.Remove(collision.collider);
            wallNormals.Remove(collision.collider);
            return;
        }

        bool hasGround = false;
        bool hasWall = false;
        Vector3 wallNormal = Vector3.zero;

        foreach (var contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                hasGround = true;
            }
            else if (Mathf.Abs(contact.normal.y) < 0.5f)
            {
                hasWall = true;
                wallNormal = contact.normal;
            }
        }

        if (hasGround) groundContacts.Add(collision.collider);
        else groundContacts.Remove(collision.collider);

        if (hasWall) wallNormals[collision.collider] = wallNormal;
        else wallNormals.Remove(collision.collider);
    }
}