using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance of the GameManager
    public static GameManager instance;
    // Example: Variables to store the player's position and other relevant data
    public Vector3 playerPosition;
    public int lastSpawnPointIndex;
    // public bool enemy1IsDead;
    // public bool enemy2IsDead;
    // ... add more variables as needed

    private void Awake()
    {
        // Implement the Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
