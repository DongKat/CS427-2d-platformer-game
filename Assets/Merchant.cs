using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Merchant : MonoBehaviour
{    
    public GameObject dialougePanel;
    public TextMeshProUGUI dialougeText;
    public string[] dialouge;
    private int index;
    public GameObject button;

    private GameManager gameManager;

    public bool haveSpoken = false;

    public bool playersisClose;
    public float WordSpeed=0.06f;

    void Start()
    {
        gameManager = GameManager.instance;

    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.E) && playersisClose && !haveSpoken)
        {
            gameManager.isShopping = true;
            if(index==0)
            {
                dialougePanel.SetActive(true);
                StartCoroutine(Type());
                index++;
            }
            else if(index < dialouge.Length)
            {
                dialougeText.text = "";
                StartCoroutine(Type());
                index++;
            }
            else
            {
                gameManager.isShopping = false;
                zeroText();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Call GameManager to open shop
            if (playersisClose && haveSpoken)
            {
                if (!gameManager.isPlayerShopping())
                    gameManager.openShop();
                else
                    gameManager.closeShop();
            }
        }

    }
    IEnumerator Type()
    {
        foreach(char letter in dialouge[index].ToCharArray())
        {
            dialougeText.text += letter;
            yield return new WaitForSeconds(WordSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            playersisClose = true;

            button.SetActive(true);


        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            playersisClose = false;

            // Show 
            button.SetActive(false);
            zeroText();
        }
    }

    public void zeroText()
    {
        haveSpoken = true;
        dialougeText.text="";
        index=0;
        dialougePanel.SetActive(false);
    }
}
