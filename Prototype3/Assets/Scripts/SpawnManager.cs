using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstacle;
    private Vector3 spawnPos = new Vector3(20, 0, 0);
    private float startDelay = 2f;
    private float spawnRate = 2f;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Get a copy of the PlayerController script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Start spawning obstacles
        InvokeRepeating("SpawnObstacle", startDelay, spawnRate);
    }

    void SpawnObstacle()
    {
        // If the game is not over spawn objects
        if (!playerControllerScript.gameOver)
        {
            Instantiate(obstacle, spawnPos, obstacle.transform.rotation);
        }
    }
}
