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

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool jumpPressed;

    private readonly HashSet<Collider> groundContacts = new HashSet<Collider>();
    private readonly Dictionary<Collider, Vector3> wallNormals = new Dictionary<Collider, Vector3>();

    private bool IsGrounded => groundContacts.Count > 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionZ
                       | RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationY
                       | RigidbodyConstraints.FreezeRotationZ;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>();
    public void OnJump(InputValue value) => jumpPressed = value.isPressed;

    private void FixedUpdate()
    {
        bool grounded = IsGrounded;

        float xVel = moveInput.x * moveSpeed;
        float yVel = rb.linearVelocity.y;

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