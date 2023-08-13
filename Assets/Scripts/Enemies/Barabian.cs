using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Barabian : MonoBehaviour
{
    [Header ("Attack parameters")]
    [SerializeField] private float attcooldown;
    [SerializeField] private float range;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    private float cooldown = Mathf.Infinity;

    [Header("Collider parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    
    [Header("Player parameters")]
    [SerializeField] private LayerMask playerLayer;
    private Transform player;

    private Vector3 initScale;
    private GameManager gameManager;
    private enemyPatrol enemy_patrol;
    private Animator anim;
    private Animator anim2;
    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        anim = GetComponent<Animator>();
        enemy_patrol = GetComponentInParent<enemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        initScale = transform.localScale;
        cooldown += Time.deltaTime;
        if (playerInSight())
        {
            if (cooldown>=attcooldown)
            {
                cooldown = 0;
                anim.SetTrigger("attack");

                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
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
        if (hit.collider != null) {
            anim2 = hit.transform.GetComponent<Animator>();
            player = hit.transform;
        }

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void killPlayer()
    {
        if (playerInSight())
        {
            gameManager.takeDamage(damage);
        }
    }
}
