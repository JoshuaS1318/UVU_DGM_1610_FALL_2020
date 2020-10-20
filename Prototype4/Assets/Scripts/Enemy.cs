using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // The speed that the enemy moves towards the player
    public float speed = 5.0f;
    // Reference to the enemies Rigidbody for physics
    private Rigidbody enemyRb;
    // Reference to the player so that it can target the player
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Get the referece of the Rigidbody
        enemyRb = GetComponent<Rigidbody>();
        // Find the player object in the scene hierarchy
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction that the enemy needs to move towards
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        // Apply the enemies movement
        enemyRb.AddForce(lookDirection * speed);

        // Destroy enemies that fall of the island
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
