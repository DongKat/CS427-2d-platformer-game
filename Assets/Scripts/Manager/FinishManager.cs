using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class FinishManager : MonoBehaviour
{
    public GameObject Bossussy;
    
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        if (Bossussy.GetComponent<enemydeath>().isDead)
        {
            gameManager.gameComplete();
        }
    }
}