using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    private float playerSpeed = 15f;
    // private float turnSpeed = 200f;

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

        // Turn the player
        RotateTowardsMouse();


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

    private void RotateTowardsMouse()
    {
        // Get the world mouse position
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 30;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 p1 = new Vector2(transform.position.x, transform.position.z);
        Vector2 p2 = new Vector2(mousePos.x, mousePos.z);

        
        float angle = (float)(Math.Atan2(Math.Abs(p1.y - p2.y), Math.Abs(p1.x - p2.x)) * (180 / Math.PI));

        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
