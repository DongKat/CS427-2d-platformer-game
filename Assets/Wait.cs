using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wait : MonoBehaviour
{
    public float wait_time = 9;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator wait_for_intro(){
        yield return new WaitForSeconds(wait_time);
        // load menu screen
        // SceneManager.LoadScence("0")
    }
}
