using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ending : MonoBehaviour
{
    public Animator animator; // Reference to the Animator controlling the animation
    public float delayTime = 30f; // Delay time after animation ends
    public string sceneToLoad; // Name of the scene to load

    private void Start()
    {
        // Start the animation
        animator.SetTrigger("StartAnimation");
        // Start the coroutine for scene transition
        StartCoroutine(TransitionAfterAnimation());
        AudioManager.StartLevelAudio();
    }

    private IEnumerator TransitionAfterAnimation()
    {
        // Wait for the animation to end
        yield return new WaitForSecondsRealtime(delayTime);

        // Load the new scene
        SceneManager.LoadScene(0);
    }
}
