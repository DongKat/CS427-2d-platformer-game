using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    [SerializeField] public int indexbuild;
    [SerializeField] public int currentIndexBuild;

    private bool levelCompleted = false;
    private SceneTransistion sceneTransistion;

    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
        sceneTransistion = FindObjectOfType<SceneTransistion>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !levelCompleted)
        {
            // finishSound.Play();
            levelCompleted = true;
            Invoke("CompleteLevel", 1f);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(indexbuild, LoadSceneMode.Single);
    }
}