using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    // Speed Lazers travel at
    public float speed = 50;

    private void Start()
    {
        StartCoroutine(DeleteAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        // Move forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // Delete the lazer after 1 second
    private IEnumerator DeleteAfterTime()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
