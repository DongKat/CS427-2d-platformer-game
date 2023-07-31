using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatPlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D collider;

    [SerializeField]
    public PistolProjectile projectilePrefab;

    [SerializeField]
    private Transform groundCheck,
        ceilCheck,
        projectileSpawnPoint;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float jumpForce = 7f;

    private float dirX = 0f;
    private SpriteRenderer sr;

    private enum MovementState
    {
        idle,
        walking,
        jumping,
        falling
    };

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        // anim=GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            // Jump
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            // Shoot
            PistolProjectile projectile = Instantiate(
                projectilePrefab,
                projectileSpawnPoint.position,
                transform.rotation
            );
            projectile.transform.localScale = transform.localScale;
        }

        // Stop animation for a while
        // UpdateAnimationState();
    }

    // private void UpdateAnimationState()
    // {
    //     MovementState currentMovementState = MovementState.idle;
    //     if (dirX > 0f)
    //     {
    //         currentMovementState = MovementState.walking;
    //         sr.flipX = false;
    //     }
    //     else if (dirX < 0f)
    //     {
    //         currentMovementState = MovementState.walking;
    //         sr.flipX = true;
    //     }
    //     else
    //     {
    //         currentMovementState = MovementState.idle;
    //     }
    //     if (rb.velocity.y > 0.0001f)
    //     {
    //         currentMovementState = MovementState.jumping;
    //     }
    //     else if (rb.velocity.y < -0.0001f)
    //     {
    //         currentMovementState = MovementState.falling;
    //     }
    //     anim.SetInteger("currentMovementState", (int)currentMovementState);
    // }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer)
            || Physics2D.BoxCast(
                collider.bounds.center,
                collider.bounds.size,
                0f,
                Vector2.down,
                .1f,
                groundLayer
            );
    }
}
