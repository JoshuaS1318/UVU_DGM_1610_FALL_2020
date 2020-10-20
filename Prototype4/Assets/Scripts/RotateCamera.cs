using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    // Speed the camera roatates
    public float rotationSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        // Get the players horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");
        // Move the camera left and right based on horizontalInput
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
