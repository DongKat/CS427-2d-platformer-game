using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    [Header("Melee attack parameters")]
    [SerializeField] private int damage;
    [SerializeField] private float attcooldown;
    [SerializeField] private float range;
    [SerializeField] private float range_y;
    private float cooldown = Mathf.Infinity;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float colliderDistance_y;

    [Header("Ranged attack parameters")]
    [SerializeField] private bossprojectile projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float colliderDistance2;
    [SerializeField] private float attcooldown2;
    [SerializeField] private float range2;
    [SerializeField] private float range_y2;

    private float cooldown2 = Mathf.Infinity;

    [Header("collider parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float speed;

    [Header("Player parameters")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform player;

    private bool walk = true;
    private GameManager gameManager;
    private Animator anim;
    private Animator anim2;
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;
        cooldown2 += Time.deltaTime;
        if(cooldown2 >= 100)
        {
            cooldown2 = 0;
            walk = false;
            anim.SetTrigger("ranged");

        }
        else if (playerInSight())
        {
            if (cooldown >= attcooldown)
            {
                walk = false;
                cooldown = 0;
                anim.SetTrigger("attack");
            }
        }
        else if (playerInSight2() && cooldown2 >= attcooldown2)
        {
            walk = false;
            cooldown2 = 0;
            anim.SetTrigger("ranged");
        }
        else if(walk)
        {
            anim.SetTrigger("walk");
            if(transform.position.x > player.position.x)
            {
                transform.localScale = new Vector2(-1,transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
    private bool playerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * transform.localScale.x * colliderDistance + transform.up * colliderDistance_y,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * range_y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
            anim2 = hit.transform.GetComponent<Animator>();

        return hit.collider != null;
    }

    private bool playerInSight2()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * transform.localScale.x * colliderDistance2,
            new Vector3(boxCollider.bounds.size.x * range2, boxCollider.bounds.size.y * range_y2, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
        {
            anim2 = hit.transform.GetComponent<Animator>();
            player = hit.transform;
        }
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * transform.localScale.x * colliderDistance + transform.up * colliderDistance_y,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * range_y, boxCollider.bounds.size.z));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * transform.localScale.x * colliderDistance2,
            new Vector3(boxCollider.bounds.size.x * range2, boxCollider.bounds.size.y * range_y2, boxCollider.bounds.size.z));
    }

    private void fireProjectile()
    {
        bossprojectile _projectile = Instantiate(
                projectilePrefab,
                projectileSpawnPoint.position,
                transform.rotation
            );
        _projectile.transform.localScale = transform.localScale;
        _projectile.GetComponent<bossprojectile>()._reset();
    }
    private void endAttack()
    {
        walk = true;
    }
    private void killPlayer()
    {
        if (playerInSight())
        {
            gameManager.takeDamage(damage);
        }
    }
}
