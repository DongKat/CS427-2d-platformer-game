using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransistion : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Example: Restore player position
        if (GameManager.instance != null)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            player.position = GameManager.instance.playerPosition;

            // GameObject enemy1 = GameObject.Find("Enemy1");
            // if (enemy1 != null)
            // {
            //     enemy1.SetActive(!GameManager.instance.enemy1IsDead);
            // }

            // GameObject enemy2 = GameObject.Find("Enemy2");
            // if (enemy2 != null)
            // {
            //     enemy2.SetActive(!GameManager.instance.enemy2IsDead);
            // }

            // ... restore other game object states as needed
        }
    }

    // Call this function before transitioning to a new scene to save the current state
    public void SaveGameState()
    {
        if (GameManager.instance != null)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            GameManager.instance.playerPosition = player.position;
            Debug.Log("SaveState");

            // Example: Save enemy states
            // GameObject enemy1 = GameObject.Find("Enemy1");
            // if (enemy1 != null)
            // {
            //     GameManager.instance.enemy1IsDead = !enemy1.activeSelf;
            // }

            // GameObject enemy2 = GameObject.Find("Enemy2");
            // if (enemy2 != null)
            // {
            //     GameManager.instance.enemy2IsDead = !enemy2.activeSelf;
            // }

            // ... save other game object states as needed
        }
    }
}
