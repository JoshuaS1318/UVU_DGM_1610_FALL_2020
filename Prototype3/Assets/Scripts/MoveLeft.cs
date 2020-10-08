using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerControllerScript;
    public float speed = 30;
    // Start is called before the first frame update
    void Start()
    {
        // Get a cop of the PlayerController script
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
    }
}
