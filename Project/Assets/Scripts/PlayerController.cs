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



    // Reference to players Rigidbody
    private Rigidbody playerRigidbody;
    // Player stats
    public float healthPoints = 100f;
    public float energy = 100f;
    private float weaponCooldown = 1f;
    private bool cooldown;

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



        // Move the player forward
        if (Input.GetMouseButton(1))
        {
            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
        }

        // Fire weapons
        if (Input.GetKeyDown(KeyCode.Space) && energy >= 0 && !cooldown)
        {
            StartCoroutine(WeaponRoutine());
        }
    }

    private void FixedUpdate()
    {
        // Turn the player
        RotateTowardsMouse();
    }

    private IEnumerator WeaponRoutine()
    {
        // Decrease the energy of the spaceship and start the cooldown
        energy -= 5;
        cooldown = true;

        // Spawn the Lazer
        Instantiate(lazerPrefab, transform.position, transform.rotation);

        yield return new WaitForSeconds(weaponCooldown);

        // End the cooldown
        cooldown = false;

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
            Destroy(other.gameObject);
            healthPoints -= 10;
        }
    }

    private void RotateTowardsMouse()
    {

        // Get the world mouse position
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 30;


        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //transform.LookAt(mousePos);

        Debug.Log(Vector3.Angle(transform.position, mousePos));

        Vector3 targetDir = mousePos - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        
        print(angle);
        //transform.Rotate(new Vector3(0, angle, 0));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, angle, 0)), 1);

    }

    private void DeathSequence()
    {
        gameObject.SetActive(false);
    }
}
