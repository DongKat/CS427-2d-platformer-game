using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton instance of the GameManager
    public static GameManager instance;

    // Get the indexbuild to reload scene when player die
    private static Finish finishPoint;

    // Example: Variables to store the player's position and other relevant data
    public Vector3 playerPosition;
    public int lastSpawnPointIndex;

    [Header("Current Values")]
    public int coinScore = 0;
    public int grenadeCount = 0;
    public int ammoCount = 0;
    public int healthCount = 100;

    [Header("Max Values")]
    public int maxAmmo = 500;
    public int maxGrenade = 10;
    public int maxHealth = 100;

    

    // public bool enemy1IsDead;
    // public bool enemy2IsDead;
    // ... add more variables as needed

    private void Awake()
    {
        // Implement the Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        finishPoint = FindObjectOfType<Finish>();
    }

    private void Update()
    {
    }

    public void SaveGameState()
    {
        // Save the player's position and other relevant data
        playerPosition = GameObject.Find("Slug").transform.position;
        lastSpawnPointIndex = finishPoint.indexbuild;
        // ... add more variables as needed
    }

    public void LoadGameState()
    {
        // Load the player's position and other relevant data
        GameObject.Find("Slug").transform.position = playerPosition;
        finishPoint.indexbuild = lastSpawnPointIndex;
        // ... add more variables as needed
    }

    public void ResetGameState()
    {
        // Reset the player's position and other relevant data
        playerPosition = Vector3.zero;
        lastSpawnPointIndex = 0;
        // ... add more variables as needed
    }



    public void addCoin(int coinScore)
    {
        coinScore += coinScore;
    }

    public void addAmmo()
    {
        ammoCount = maxAmmo;
    }

    public void addGrenade()
    {
        grenadeCount = maxGrenade;
    }

    public void addScore(int score)
    {
        coinScore += score;
    }

    public void addHealth()
    {
        healthCount = maxHealth;
    }

    public void takeDamage(int damage)
    {
        healthCount -= damage;
    }

    public bool isPlayerDead()
    {
        return healthCount <= 0;
    }
    
}
