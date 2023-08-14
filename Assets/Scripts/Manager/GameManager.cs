using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Singleton instance of the GameManager
    public static GameManager instance;

    public bool isGod;

    // Get the indexbuild to reload scene when player die
    private static Finish finishPoint;
    private static UIManager uiManager;
    private static AudioManager audioManager;

    public PlayerController player;

    private bool isGameOver = false;
    public bool isGameVictory = false;


    // Player's position
    public Vector3 playerPosition;
    public int lastSpawnPointIndex = 0;

    public bool isShopping = false;


    [Header("Current Values")]
    public int coinScore = 0;
    public int money = 0;
    public int grenadeCount = 10;
    public int ammoCount = 0;
    public float healthCount = 100f;

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
        player = FindObjectOfType<PlayerController>();
        
        uiManager = UIManager.instance;


        UIManager.UpdateAmmoUI();
        UIManager.UpdateBombsUI();
        UIManager.UpdateScoreUI();
        UIManager.UpdateHealthUI();
    }

    private void Update()
    {
        if (isGod)
        {
            grenadeCount = maxGrenade;
            healthCount = maxHealth;
        }

        moneyUpdate();
        UIManager.UpdateAmmoUI();
        UIManager.UpdateBombsUI();
        UIManager.UpdateScoreUI();
        UIManager.UpdateHealthUI();
    }


    public void gameOver()
    {
        // When player died
        isGameOver = true;

        // player.playDeath();

        UIManager.ShowGameOverPanel();
        AudioManager.PlayGameOverAudio();

        StartCoroutine(WaitResetLevel());
    }

    public void gameComplete()
    {
        // When player reach finish point
        isGameVictory = true;

        // player.playVictory();

        UIManager.ShowGameCompletePanel();
        AudioManager.PlayLevelCompleteAudio();

        StartCoroutine(WaitReturnToMenu());
    }

    
    public void ResetLevel()
    {
        // Reset player's position
        playerPosition = Vector3.zero;

        isGameOver = false;

        // Reset player's values
        coinScore = 0;
        money = 0;
        grenadeCount = 10;
        ammoCount = 0;
        healthCount = 100;

        // Reset UI
        UIManager.UpdateAmmoUI();
        UIManager.UpdateBombsUI();
        UIManager.UpdateScoreUI();
        UIManager.UpdateHealthUI();

        // Load scene again
        SceneManager.LoadScene(finishPoint.currentIndexBuild);
    }

    public void ReturnToMenu()
    {
        // Hard code to load end scene
        SceneManager.LoadScene(4);
    }

    public void addCoin(int score)
    {
        coinScore += score;
        money += score;
        UIManager.UpdateScoreUI();
        UIManager.UpdateCoinUI();
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
        if (isPlayerDead())
            return;
        healthCount -= damage;

        UIManager.UpdateHealthUI();

        if (healthCount <= 0)
        {
            gameOver();
        }

    }

    public void takeDamage(float damage)
    {
        if (isPlayerDead())
            return;
        healthCount -= damage;
        UIManager.UpdateHealthUI();

        if (healthCount <= 0)
        {
            gameOver();
        }

    }


    public void throwGrenade()
    {
        grenadeCount--;
        UIManager.UpdateBombsUI();
    }

    public int getAmmo()
    {
        return ammoCount;
    }

    public int getGrenade()
    {
        return grenadeCount;
    }

    public float getHealth()
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

    private IEnumerator WaitResetLevel()
    {
        yield return new WaitForSecondsRealtime(5f);
        ResetLevel();
    }
    private IEnumerator WaitReturnToMenu()
    {
        yield return new WaitForSecondsRealtime(6f);
        ReturnToMenu();
    }

    public bool isPlayerShopping()
    {
        return isShopping;
    }

    public bool isPlayerVictory()
    {
        return isGameVictory;
    }

    public void openShop()
    {
        UIManager.ShowShopPanel();
        instance.isShopping = true;
    }

    public void closeShop()
    {
        UIManager.HideShopPanel();
        instance.isShopping = false;
    }

    public void moneyUpdate()
    {
        UIManager.UpdateCoinUI();
    }
}
