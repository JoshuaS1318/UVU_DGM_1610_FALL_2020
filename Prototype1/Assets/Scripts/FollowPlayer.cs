using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 5, -7);

    // Update is called once per frame
    void Update()
    {
        // Move the camera to the players position and add an offset
        transform.position = player.transform.position + offset;
    }
}
