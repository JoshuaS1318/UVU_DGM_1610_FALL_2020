using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Reference to the payers RigidBody
    private Rigidbody playerRB;
    // Bool for if the game is over
    public bool gameOver = false;
    public float jumpForce = 15f;
    public float gravityModifier = 3f;

    // Bool for if the player is on the ground or not
    private bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        // Get the reference to the RigidBody
        playerRB = GetComponent<Rigidbody>();
        // Apply the gravity modifier
        Physics.gravity *= gravityModifier;
    }
    
    // Update is called once per frame
    void Update()
    {
        // Add upwards force to the player when the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround) {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player is touching the ground turn isOnGround to true
        if (collision.gameObject.CompareTag("Ground")) {
            isOnGround = true;
        // If the player is touching a obstacle end the game
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
        }
    }
}
