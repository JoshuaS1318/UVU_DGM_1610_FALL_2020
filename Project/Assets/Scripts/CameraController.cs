using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Get a reference to the player
    public GameObject player;

    // Provide camera offset
    private Vector3 cameraOffset = new Vector3(0, 0, -20);

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + cameraOffset;
    }
}
