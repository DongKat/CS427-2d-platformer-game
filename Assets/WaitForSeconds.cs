using UnityEngine;
using System.Collections;

public class WaitForSeconds : MonoBehaviour
{
    public WaitForSeconds(float seconds){
        // wait for seconds

    }

    void Start()
    {
        Debug.Log("Started " + Time.time);
        StartCoroutine(ExampleCoroutine());
        Debug.Log("Finished " + Time.time);

    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}