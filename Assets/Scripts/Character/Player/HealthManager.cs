using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public bool isDead = false;

    [Header("Health")]
    public int health = 100;
    public int maxHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((health <= 0) && !isDead)
        {
            isDead = true;
        }
        
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
        }
    }

    public void heal()
    {
        health = maxHealth;
    }
}
