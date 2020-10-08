using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 spawnPos;
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        // Get the starting position of the background
        spawnPos = transform.position;

        // Get half the width of the background to find when to restart it
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Reset position if it hits makes it repeatWidth distance
        if (transform.position.x < spawnPos.x - repeatWidth)
        {
            transform.position = spawnPos;
        }
    }
}
