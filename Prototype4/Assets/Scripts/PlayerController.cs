using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // The speed that the player moves at
    public float speed = 5.0f;
    // The knockback multiplyer given by the powerup
    public float powerupStrength = 15.0f;
    // Whether or not the player has the powerup active
    public bool hasPowerup;
    // Reference to the players powerup indicator
    public GameObject powerupIndicator;
    // Reference to the players Rigidbody for movement
    private Rigidbody playerRb;
    // Reference to the focalPoint to determine direction to move in
    private GameObject focalPoint;

    // Start is called before the first frame update
    void Start()
    {
        // Get the reference to the players Rigidbody
        playerRb = GetComponent<Rigidbody>();
        // Find the focalPoint in the scene hierachy
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        // Get the players vertical input
        float forwardInput = Input.GetAxis("Vertical");
        // Apply the movement to the player
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        // Move the powerup indicator to the player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.25f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If entering the powerups trigger
        if (other.CompareTag("Powerup"))
        {
            // Destroy the powerup and start the powerup count down routine
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    /// <summary>
    /// Activates the powerup waits 7 seconds then disables it again
    /// </summary>
    /// <returns></returns>
    IEnumerator PowerupCountdownRoutine()
    {
        // Activate powerup and show powerup indicator
        hasPowerup = true;
        powerupIndicator.SetActive(true);

        // Wait seven seconds
        yield return new WaitForSeconds(7);

        // Disable powerup and hid powerup indicator
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If collliding with the enemy and powerup is active
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            // Get the enemies Rigidbody
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();

            // Calculate what direction to send the player
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            // Move the enemy
            enemyRigidBody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

            // Log a message that says that the player has collided with the enemy with an active powerup
            // Debug.Log("Collided with gameObject: " + collision.gameObject.name + "with hasPowerup set to " + hasPowerup);
        }
    }
}
