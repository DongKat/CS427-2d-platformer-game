using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints; // Array to store all the spawn points


    public void RespawnPlayer()
    {
        // Choose a random spawn point index (excluding the first index which is the parent Transform)
        int randomIndex = 2;
        spawnPoints = GetComponentsInChildren<GameObject>();
        Debug.Log("Spawn points: " + spawnPoints.Length);
        // Set the player's position to the chosen spawn point's position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = spawnPoints[randomIndex].transform.position;
            Debug.Log("Respawned at: " + spawnPoints[randomIndex].transform.position);
        }
    }
}
