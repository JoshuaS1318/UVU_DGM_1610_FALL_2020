using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // The amount of points the player gets or loses for clicking this target
    public int pointValue;
    // The explosion particle played when the target is clicked
    public ParticleSystem explosionParticle;

    // Reference to the GameManager script
    private GameManager gameManager;
    // Reference to the targets rigid body
    private Rigidbody targetRb;
    // Parameters for the targets being launched
    private float minSpeed = 12; private float maxSpeed = 16;
    private float maxTorque = 10; private float xRange = 4;
    private float ySpawnPos = -2;


    // Start is called before the first frame update
    void Start()
    {
        // Get the reference for the GameManager
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // Get the reference for the rigid body
        targetRb = GetComponent<Rigidbody>();

        // Launch this target
        targetRb.AddForce(RandomForce() , ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }   
    
    /// <summary>
    /// Get a random force
    /// </summary>
    /// <returns>The random force</returns>
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    /// <summary>
    /// Get a random torque for the target
    /// </summary>
    /// <returns>The random torque</returns>
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    /// <summary>
    /// Get a random spawn position and return it
    /// </summary>
    /// <returns>The random position</returns>
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy the target if it falls of the bottom
        Destroy(gameObject);
        // If the target was not a bad target end the game
        if (!gameObject.CompareTag("Bad")) { gameManager.GameOver(); }
    }

    /// <summary>
    /// Destroy a target and give its points if it is clicked
    /// </summary>
    private void OnMouseDown()
    {
        // If the game is active
        if (gameManager.isGameActive)
        {
            // Destroy the target
            Destroy(gameObject);
            // Play the explosion
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            // Update the score
            gameManager.UpdateScore(pointValue);
        }
    }
}
