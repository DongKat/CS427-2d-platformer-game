using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sr;
    private Animator anim;

    // Floats
    private float dirX = 0f;
    private float dirY = 0f;
    private float nextFire = 0f;

    // Booleans
    // private bool isLookingUp = false;
    // private bool isLookingDown = false;
    private bool isGrounded = false;

    // private bool isCrouching = false;
    private bool isWalking = false;
    private bool isJumping = false;
    private bool isFalling = false;

    private bool isFiring = false;

    // private bool isMelee = false;
    private bool isThrowingGrenade = false;

    [Header("Time shoot")]
    public float shootTime = 0.0f;
    public float fireRate = 0.5F;

    [Header("Spawn Points")]
    public Transform projectileSpawnPoint;
    public Transform grenadeSpawnPoint;
    public Transform groundCheck;

    // [Header("Melee Check")]
    // public Transform meleeZone;

    [Header("Ground Check")]
    public LayerMask groundLayer;

    [Header("Projectiles")]
    public ProjectileBehaviour projectilePrefab;
    public ProjectileBehaviour grenadePrefab;

    [Header("Movement")]
    public float moveSpeed = 7f;
    public float jumpForce = 7f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Get input from player
        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");

        // Move (horizontal)
        if (dirX > 0f)
        {
            // Moving right
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isWalking = true;
        }
        else if (dirX < 0f)
        {
            // Moving left
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            isWalking = true;
        }
        else
        {
            // Idle
            rb.velocity = new Vector2(0, rb.velocity.y);
            isWalking = false;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            // Jump
            Debug.Log("Jump");
            isJumping = true;
            isFalling = false;
            Jump();
        }
        if (rb.velocity.y < 0)
        {
            // Falling
            // Debug.Log("Velocity < 0");
            isJumping = false;
            isFalling = true;
        }
        else
        {
            // Debug.Log("Velocity > 0");
            isFalling = false;
        }

        // Shoot
        if (Input.GetButtonDown("Fire1"))
        {
            isFiring = true;
            Fire();
        }
        else
        {
            isFiring = false;
        }

        // Throw grenade;
        if (Input.GetButtonDown("Fire2"))
        {
            isThrowingGrenade = true;
            ThrowGrenade();
        }
        else
        {
            isThrowingGrenade = false;
        }
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        // Debug print all variables
        // Debug.Log("isWalking: " + isWalking + " isJumping: " + isJumping + " isFalling: " + isFalling + " isGrounded: " + IsGrounded() + " isFiring" + isFiring + " isThrowingGrenade: " + isThrowingGrenade);

        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isFalling", isFalling);
        anim.SetBool("isGrounded", IsGrounded());
        // anim.SetBool("isMelee", isMelee);
        anim.SetBool("isFiring", isFiring);
        anim.SetBool("isThrowingGrenade", isThrowingGrenade);
        // anim.SetBool("isCrouching", isCrouching);
        // anim.SetBool("isLookingUp", isLookingUp);
        // anim.SetBool("isLookingDown", isLookingDown);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            ProjectileBehaviour projectile = Instantiate(
                projectilePrefab,
                projectileSpawnPoint.position,
                transform.rotation
            );
            projectile.transform.localScale = transform.localScale;
        }
    }

    private void ThrowGrenade()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            ProjectileBehaviour grenade = Instantiate(
                grenadePrefab,
                grenadeSpawnPoint.position,
                transform.rotation
            );
            grenade
                .GetComponent<Rigidbody2D>()
                .AddForce(transform.right * 10f, ForceMode2D.Impulse);
            grenade.transform.localScale = transform.localScale;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
