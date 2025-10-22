using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    [Header("Jump Settings")]
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float secondJumpForce = 10f;
    [SerializeField] private float jumpCooldown = 0.5f;
    [SerializeField] private bool canDoubleJump = false;

    private float jumpCooldownTimer;
    private bool isJumping;
    private int jumpsLeft = 1;

    private PlayerInputs inputs;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        inputs = new PlayerInputs();
        inputs.Enable();

        inputs.Abilities.Jump.performed += _ => HandleJump();
    }

    private void OnDestroy()
    {
        inputs.Abilities.Jump.performed -= _ => HandleJump();
        inputs.Disable();
    }

    private void Update()
    {
        UpdateJumpCooldown();
    }

    private void HandleJump()
    {
        if (IsGrounded())
        {
            jumpsLeft = 1;
            PerformJump(jumpForce);
        }
        else if (jumpsLeft > 0 && !isJumping && jumpCooldownTimer <= 0f && canDoubleJump)
        {
            PerformJump(secondJumpForce);
            jumpsLeft--;
            isJumping = true;
            jumpCooldownTimer = jumpCooldown;
        }
    }

    private void PerformJump(float force)
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.y = force;
        rb.linearVelocity = velocity;
    }

    private void UpdateJumpCooldown()
    {
        if (jumpCooldownTimer > 0f)
        {
            jumpCooldownTimer -= Time.deltaTime;
            if (jumpCooldownTimer <= 0f)
                isJumping = false;
        }
    }

    private bool IsGrounded()
    {
        Vector2 boxSize = coll.bounds.size;
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, boxSize, 0f, Vector2.down, 0.1f, jumpableGround);
        return hit.collider != null;
    }
}
