using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    public static GameManager gameManager;

    public Slider healthbar;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bombs;
    public TextMeshProUGUI ammoText;

    public TextMeshProUGUI gameOverPanel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;

        // set score text to 0
        UpdateScoreUI();
        UpdateBombsUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public static void UpdateScoreUI()
    {
        //If there is no current UIManager, exit
        if (instance == null)
            return;

        //Refresh the score
        instance.scoreText.SetText(gameManager.coinScore.ToString());
    }

    public static void UpdateBombsUI()
    {
        //If there is no current UIManager, exit
        if (instance == null)
            return;

        //Refresh the score
        instance.bombs.SetText(gameManager.grenadeCount.ToString());
    }

    public static void UpdateHealthUI()
    {
        //If there is no current UIManager, exit
        if (instance == null)
            return;

        //update the player death count element
        instance.healthbar.value = gameManager.healthCount;
    }

    public static void UpdateAmmoUI()
    {
        //If there is no current UIManager, exit
        if (instance == null)
            return;

        //Refresh the score
        instance.ammoText.SetText("oo");
    }

    public static void ShowGameOverPanel()
    {
        //If there is no current UIManager, exit
        if (instance == null)
            return;

        //Show the game over panel
        instance.gameOverPanel.gameObject.SetActive(true);
    }


}
