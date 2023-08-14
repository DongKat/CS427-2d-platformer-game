using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fatzombie : MonoBehaviour
{
    [Header("Attack parameters")]
    [SerializeField] private projectile projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float attcooldown;
    [SerializeField] private float range;
    private float cooldown = Mathf.Infinity;

    [Header("Collider parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player parameters")]
    [SerializeField] private LayerMask playerLayer;

    private enemyPatrol enemy_patrol;
    private Animator anim;
    private Animator anim2;
    void Start()
    {
        anim = GetComponent<Animator>();
        enemy_patrol = GetComponentInParent<enemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;
        if (playerInSight())
        {
            if (cooldown >= attcooldown)
            {
                cooldown = 0;
                anim.SetTrigger("attack");
            }
        }
        else if (enemy_patrol == null)
        {
            anim.SetTrigger("idle");
        }
        if (enemy_patrol != null)
        {
            enemy_patrol.enabled = !playerInSight();
        }
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
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
    }
}
