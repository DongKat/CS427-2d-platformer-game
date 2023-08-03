using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatPlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sr;

    [SerializeField]
    private Transform groundCheck,
        ceilCheck,
        projectileSpawnPoint,
        grenadeSpawnPoint;

    [SerializeField]
    private LayerMask groundLayer;

    [Header("Projectiles")]
    public ProjectileBehaviour projectilePrefab, grenadePrefab;

    [Header("Movement")]
    private float moveSpeed = 7f;
    private float jumpForce = 7f;

    private float dirX = 0f;

    private bool isLookingUp = false;
    private bool isLookingDown = false;
    private bool isGrounded = false;
    private bool isCrouching = false;
    private bool isWalking = false;
    private bool isJumping = false;
    private bool isFiring = false;
    private bool isThrowingGrenade = false;

    private enum MovementState
    {
        idle,
        crouching,
        firing,
        throwingGrenade,
        walking,
        jumping,
        falling
    };


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        // anim=GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Get input from player
        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (!Mathf.Approximately(0, dirX))
        {
            // Move (horizontal)
            transform.rotation = dirX < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            // Jump
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // Shoot
            ProjectileBehaviour projectile = Instantiate(
                projectilePrefab,
                projectileSpawnPoint.position,
                transform.rotation
            );
            projectile.transform.localScale = transform.localScale;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            // Throw grenade;
            ProjectileBehaviour grenade = Instantiate(
                grenadePrefab,
                projectileSpawnPoint.position,
                transform.rotation
            );
            grenade.transform.localScale = transform.localScale;
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState currentMovementState = MovementState.idle;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        print("Colliding");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.tag == "Coin")
        // {
        //     Destroy(collision.gameObject);
        // }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer)
            || Physics2D.BoxCast(
                GetComponent<Collider>().bounds.center,
                GetComponent<Collider>().bounds.size,
                0f,
                Vector2.down,
                .1f,
                groundLayer
            );
    }
}