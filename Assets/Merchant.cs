using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Merchant : MonoBehaviour
{    
    public GameObject dialougePanel;
    public Text dialougeText;
    public string[] dialouge;
    private int index;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playersisClose)
        {
            if(dialougePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialougePanel.SetActive(true);
                dialougeText.text = dialouge[index];
            }
        }
    }
    private onTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // dialougePanel.SetActive(true);
            // dialougeText.text = dialouge[index];
            playersisClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // dialougePanel.SetActive(false);
            playersisClose = false;
            zeroText();
        }
    }

    public void zeroText()
    {
        dialougeText.text=""
        index=0
        dialougePanel.SetActive(false);
    }
}
