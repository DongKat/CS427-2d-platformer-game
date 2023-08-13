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
    private static UIManager uiManager;
    private static AudioManager audioManager;


    // Player's position
    public Vector3 playerPosition;
    public int lastSpawnPointIndex = 0;


    [Header("Current Values")]
    public int coinScore = 0;
    public int grenadeCount = 10;
    public int ammoCount = 0;
    public int healthCount = 100;

    [Header("Max Values")]
    public int maxAmmo = 500;
    public int maxGrenade = 10;
    public int maxHealth = 100;

    [Header("Audio Settings")]
    public float bgmVolume = 1f;
    public float sfxVolume = 1f;

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
        
        uiManager = UIManager.instance;


        UIManager.UpdateAmmoUI();
        UIManager.UpdateBombsUI();
        UIManager.UpdateScoreUI();
        UIManager.UpdateHealthUI();
    }

    private void Update()
    {
        if (isPlayerDead())
        {
            
        }
    }

    
    public void ResetGameState()
    {
        // Reset the player's position and other relevant data
        playerPosition = Vector3.zero;
        lastSpawnPointIndex = 0;
        // ... add more variables as needed
    }

    public void addCoin(int score)
    {
        coinScore += score;
        UIManager.UpdateScoreUI();
    }

    public void addAmmo()
    {
        ammoCount = maxAmmo;
        grenadeCount = maxGrenade;
        UIManager.UpdateAmmoUI();
        UIManager.UpdateBombsUI();
    }

    public void addGrenade()
    {
        grenadeCount = maxGrenade;
        UIManager.UpdateBombsUI();
    }

    public void addScore(int score)
    {
        coinScore += score;
        UIManager.UpdateScoreUI();
    }

    public void addHealth()
    {
        healthCount = maxHealth;
        UIManager.UpdateHealthUI();
    }

    public void takeDamage(int damage)
    {
        healthCount -= damage;
        UIManager.UpdateHealthUI();
    }

    public void throwGrenade()
    {
        grenadeCount--;
        // UIManager.UpdateBombsUI();
    }

    public int getAmmo()
    {
        return ammoCount;
    }

    public int getGrenade()
    {
        return grenadeCount;
    }

    public int getHealth()
    {
        return healthCount;
    }

    public int getScore()
    {
        return coinScore;
    }

    public bool isPlayerDead()
    {
        return healthCount <= 0;
    }
}
