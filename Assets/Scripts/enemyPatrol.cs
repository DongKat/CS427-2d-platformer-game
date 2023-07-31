using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    [Header ("Patrol point")]
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    private int leftDir = -1;
    private int rightDir = 1;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool moveChange;

    [Header("Idle animation")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy animator")]
    [SerializeField] private Animator anim;
    void Awake()
    {
        initScale = enemy.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool("idle", false);
    }

    private void Update()
    {
        if (moveChange)
        {
            if (enemy.position.x >= left.position.x)
                moveDirection(leftDir);
            else
                changeDirection();
        }
        else
        {
            if (enemy.position.x <= right.position.x)
                moveDirection(rightDir);
            else
                changeDirection();
        }
    }

    private void changeDirection()
    {
        anim.SetTrigger("idle");
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        {
            moveChange = !moveChange;
        }
    }


    private void moveDirection(int moveDir)
    {
        idleTimer = 0;
        anim.SetTrigger("running");

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * moveDir,
            initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * moveDir * speed,
            enemy.position.y, enemy.position.z);
    }
}
