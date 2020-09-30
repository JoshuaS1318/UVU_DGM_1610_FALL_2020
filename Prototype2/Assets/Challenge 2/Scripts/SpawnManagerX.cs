using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private float spawnLimitXLeft = -22;
    private float spawnLimitXRight = 7;
    private float spawnPosY = 30;

    private float startDelay = 1.0f;
    private float spawnInterval = 3.0f;
    private float lowerRandomTime = 0.5f;
    private float higherRandomTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomBall", startDelay, spawnInterval);
    }

    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall()
    {
        // Wait for random timer to spawn animal
        StartCoroutine(RandomTimer());
    }

    // Starts a timer for a random amount of time to wait before spawning an animal
    IEnumerator RandomTimer()
    {

        // Wait for random amount of time
        yield return new WaitForSeconds(Random.Range(lowerRandomTime, higherRandomTime));

        // Random index for what ball to use
        int ballIndex = Random.Range(0, 3);

        // Generate random ball index and random spawn position
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

        // instantiate ball at random spawn location
        Instantiate(ballPrefabs[ballIndex], spawnPos, ballPrefabs[ballIndex].transform.rotation);
    }
}
