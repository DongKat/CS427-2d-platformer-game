using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
    // }
    // Update is called once per frame
    // void Update()
    // {
    // }
    public void newGame()
    {
        SceneManager.LoadScene(1);
    }
    public void go2Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void exit()
    {
        Application.Quit();
    }
}
