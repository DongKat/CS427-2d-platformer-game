using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sr;
    private Animator anim;

    private static GameManager gameManager;

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

    public bool isDead = false;
    public bool isVictory = false;
    public bool isShopping = false;

    [Header("Time shoot")]
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

        gameManager = GameManager.instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap-Spike")
        {
            if (!isDead)
            {
                Debug.Log("Player hit spike");
                takeDamage(100);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        checkEnd();
        if (isShopping || isVictory || isDead)
            return;
        else
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
            if (Input.GetButtonDown("Fire2") && gameManager.grenadeCount > 0)
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

    private void checkEnd()
    {
        if (gameManager.isPlayerDead() && !isDead)
        {
            isDead = true;
            playDeath();
            gameManager.gameOver();
        }
        else if (gameManager.isPlayerVictory() && !isVictory)
        {
            isVictory = true;
            playVictory();
            gameManager.gameComplete();
        }
    }

    private void checkShopping()
    {
        if (gameManager.isPlayerShopping())
        {
            isShopping = true;
        }
        else
        {
            isShopping = false;
        }
    }

    public void playDeath()
    {
        Debug.Log("Player is dead");
        anim.Play("Death_2");
        AudioManager.PlayDeathAudio();
    }

    public void playVictory()
    {
        isVictory = true;
        anim.SetBool("isVictory", isVictory);
        AudioManager.PlayLevelCompleteAudio();
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
        AudioManager.PlayNormalShotAudio();
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

            // Reduce grenade count
            gameManager.throwGrenade();
        }
    }

    private void takeDamage(int damage)
    {
        gameManager.takeDamage(damage);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);    }
}
