using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    // Speed player moves per second
    public float speed = 30;

    // Reference to players controller script
    private PlayerController playerControllerScript;
    // Bound on left to destroy obstacles
    private float leftBound = -10f;

    // Start is called before the first frame update
    void Start()
    {
        // Get a copy of the PlayerController script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player forward if the game is not over
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        // Destroy Obstacles that have passed the left bound
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
