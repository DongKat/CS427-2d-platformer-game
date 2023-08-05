using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; // Array to store all the spawn points

    public void RespawnPlayer()
    {
        // Choose a random spawn point index (excluding the first index which is the parent Transform)
        
        int randomIndex = Random.Range(0, spawnPoints.Length);

        // Set the player's position to the chosen spawn point's position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Debug.Log(randomIndex);
            player.transform.position = spawnPoints[randomIndex].position;
        }
    }
}
