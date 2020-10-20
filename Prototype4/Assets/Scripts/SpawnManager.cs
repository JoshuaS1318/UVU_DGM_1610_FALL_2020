using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // The number of enemies to spawn per wave
    public int waveNumber = 1;
    // The number of enemies currently in the scene
    public int enemyCount;
    // Reference to the Enemy prefab
    public GameObject enemyPrefab;
    // Reference to teh Powerup prefab
    public GameObject powerupPrefab;
    // The range in which new enemies and powerups will be able to spawn in
    private float spawnRange = 9.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn the enemies and powerups for wave one
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        // Set the enemyCount to the number of Enemy scripts still in the scene
        enemyCount = FindObjectsOfType<Enemy>().Length;

        // If all the enemies fall off spawn the next wave with a new powerup
        if (enemyCount == 0)
        {
            // Increase the wave number
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    /// <summary>
    /// Spawns enemies in random positions
    /// </summary>
    /// <param name="enemiesToSpawn">The number of enemies spawned in each wave</param>
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // Spawn enemies the given number of times
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    /// <summary>
    /// Returns a random Vector3 within the bounds of spawnRange
    /// </summary>
    /// <returns>A random Vector3</returns>
    private Vector3 GenerateSpawnPosition()
    {
        // Get random x and z ppositions
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        // Return the new Vector3
        return new Vector3(spawnPosX, 0.125f, spawnPosZ);
    }
}
