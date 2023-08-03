using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stupidcrab : MonoBehaviour
{
    [Header("Melee attack parameters")]
    [SerializeField] private float attcooldown;
    [SerializeField] private float range;
    private float cooldown = Mathf.Infinity;
    [SerializeField] private float colliderDistance;

    [Header("Ranged attack parameters")]
    [SerializeField] private projectile projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float colliderDistance2;
    [SerializeField] private float attcooldown2;
    [SerializeField] private float range2;
    private float cooldown2 = Mathf.Infinity;

    [Header("collider parameters")]
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player parameters")]
    [SerializeField] private LayerMask playerLayer;

    private enemyPatrol enemy_patrol;
    private Animator anim;
    private Animator anim2;
    void Awake()
    {
        anim = GetComponent<Animator>();
        enemy_patrol = GetComponentInParent<enemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;
        cooldown2 += Time.deltaTime;
        if (playerInSight())
        {
            if (cooldown >= attcooldown)
            {
                cooldown = 0;
                anim.SetTrigger("attack");
            }
        }
        else if (playerInSight2())
        {
            if (cooldown2 >= attcooldown2)
            {
                cooldown2 = 0;
                anim.SetTrigger("ranged");
            }
        }
        enemy_patrol.enabled = !playerInSight()  && !playerInSight2();
    }
    private bool playerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
            anim2 = hit.transform.GetComponent<Animator>();

        return hit.collider != null;
    }

    private bool playerInSight2()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * transform.localScale.x * colliderDistance2,
            new Vector3(boxCollider.bounds.size.x * range2, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
            anim2 = hit.transform.GetComponent<Animator>();

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * transform.localScale.x * colliderDistance2,
            new Vector3(boxCollider.bounds.size.x * range2, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void fireProjectile()
    {
        projectile _projectile = Instantiate(
                projectilePrefab,
                projectileSpawnPoint.position,
                transform.rotation
            );
        _projectile.transform.localScale = transform.localScale;
        _projectile.GetComponent<projectile>()._reset();
        //_projectile.GetComponent<BoxCollider2D>().enabled = true;
        //_projectile.GetComponent<projectile>().hit = false;
    }

    private void killPlayer()
    {
        if (playerInSight())
        {
            Debug.Log("killing");
            anim2.SetTrigger("Slug falling");
        }
    }
}
