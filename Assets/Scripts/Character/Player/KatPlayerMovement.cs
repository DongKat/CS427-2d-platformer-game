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
        projectileSpawnPoint;

    [SerializeField]
    private LayerMask groundLayer;

    [Header("Projectiles")]
    public PistolProjectile projectilePrefab;
    public GrenadeProjectile grenadePrefab;

    [Header("Movement")]
    private float moveSpeed = 7f;
    private float jumpForce = 7f;

    private float dirX = 0f;

    public enum CollectibleType
    {
        Grenade,
        MedKit
    }

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
        coll = GetComponent<BoxCollider2D>();
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Throw grenade;
            GrenadeProjectile grenade = Instantiate(
                grenadePrefab,
                projectileSpawnPoint.position,
                transform.rotation
            );
            grenade.transform.localScale = transform.localScale;
        }

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
