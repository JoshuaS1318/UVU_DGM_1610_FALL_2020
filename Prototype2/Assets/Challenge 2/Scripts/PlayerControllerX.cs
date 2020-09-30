using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private bool cooldown = false;
    private float cooldownTime = 1;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && !cooldown)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            // Start cooldown
            StartCoroutine(Cooldown());
        }
    }

    // Cooldown for cooldownTime seconds between being able to spawn dogs
    IEnumerator Cooldown()
    {
        cooldown = !cooldown;
        yield return new WaitForSeconds(cooldownTime);
        cooldown = !cooldown;
    }
}
