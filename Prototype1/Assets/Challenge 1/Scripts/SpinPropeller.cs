using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPropeller : MonoBehaviour
{
    private float spinSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        // Spin the propeller by spinSpeed
        transform.Rotate(Vector3.forward * Time.deltaTime * spinSpeed);
    }
}
