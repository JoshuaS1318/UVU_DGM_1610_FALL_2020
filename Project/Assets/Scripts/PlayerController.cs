using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    private float playerSpeed = 15f;
    private float turnSpeed = 200f;

    // TEMPORARY -- I will eventually make more formal boundaries however I plan on 
    // adding camera controls as well as more formal boundaries later
    private float playArea = 100f;

    // Player stats
    public float healthPoints = 100f;


    // Update is called once per frame
    void Update()
    {
        // Player died and the game is over
        if (healthPoints < 0)
        {
            return;
        }

        // Player is going out of bounds
        if (transform.position.x > playArea || transform.position.x < -playArea || transform.position.z > playArea || transform.position.z < -playArea)
        {
            healthPoints -= 100;
        }

        // TODO: Make the player point towards the mouse
        // Turn the player
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * turnSpeed * Time.deltaTime);
        }

        // Move the player forward
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            healthPoints -= 100;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            healthPoints -= 10;
        }
    }

}
