using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemydeath : MonoBehaviour
{
    // Start is called before the first frame update
    private enemyPatrol enemy_patrol;
    private Animator anim;
    [SerializeField] private int score;
    [SerializeField] private int health;
    private GameManager gameManager;

    private bool isDead = false;

    [SerializeField]
    private Behaviour[] components;

    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        anim = GetComponent<Animator>();
        enemy_patrol = GetComponentInParent<enemyPatrol>();
    }
    public void takeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            death();
        }
    }

    private void death()
    {
        if (!isDead)
            isDead = true;
        else
            return;
        anim.SetTrigger("death");
        gameManager.addScore(score);
        foreach (Behaviour component in components)
        {
            component.enabled = false;
        }
    }

    private void gone()
    {
        Destroy(enemy_patrol.gameObject);
        Destroy(gameObject);
    }
}
