using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    [SerializeField] private int indexbuild;

    private bool levelCompleted = false;
    private SceneTransistion sceneTransistion;

    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
        sceneTransistion = FindObjectOfType<SceneTransistion>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Slug" && !levelCompleted)
        {
            // finishSound.Play();
            levelCompleted = true;
            Invoke("CompleteLevel", 1f);
        }
    }

    private void CompleteLevel()
    {
        sceneTransistion.SaveGameState();
        SceneManager.LoadScene(indexbuild, LoadSceneMode.Single);
    }
}