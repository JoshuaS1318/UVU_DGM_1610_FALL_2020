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
    private float playerSpeed = 8f;

    // TODO private float turnSpeed = 200f;

    // TEMPORARY -- I will eventually make more formal boundaries however I plan on 
    private float playArea = 100f;

    // Reference to players Rigidbody
    private Rigidbody playerRigidbody;
    // Player stats
    public float healthPoints = 100f;

    // Weapons
    public GameObject lazerPrefab;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player died and the game is over
        if (healthPoints <= 0)
        {
            DeathSequence();
        }

        // Player is going out of bounds
        if (transform.position.x > playArea || transform.position.x < -playArea || transform.position.z > playArea || transform.position.z < -playArea)
        {
            healthPoints -= 100;
        }

        // Turn the player
        RotateTowardsMouse();

        // Move the player forward
        if (Input.GetMouseButton(1))
        {
            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
        }

        // Fire weapons
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(lazerPrefab, transform.position, transform.rotation);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyLazer"))
        {
            healthPoints -= 10;
            Destroy(other.gameObject);
        }
        
    }

    private void RotateTowardsMouse()
    {

        // Get the world mouse position
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 30;


        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.LookAt(mousePos);

        //Vector2 p1 = new Vector2(transform.position.x, transform.position.z);
        //Vector2 p2 = new Vector2(mousePos.x, mousePos.z);

        
        //float angle = (float)-(Math.Atan2(p1.y - p2.y, p1.x - p2.x) * (180 / Math.PI));
        

        //transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void DeathSequence()
    {
        gameObject.SetActive(false);
    }
}
