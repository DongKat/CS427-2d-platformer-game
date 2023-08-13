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

    public bool playersisClose;
    public float WordSpeed=0.06f;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playersisClose)
        {
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
                zeroText();
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
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            playersisClose = false;
            zeroText();
        }
    }

    public void zeroText()
    {
        dialougeText.text="";
        index=0;
        dialougePanel.SetActive(false);
    }
}
