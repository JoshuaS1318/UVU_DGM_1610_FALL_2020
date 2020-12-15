using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // handle collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lazer"))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("EnemyLazer"))
        {
            Destroy(other.gameObject);
        }
    }

    // Destroy self
    public void DeathSequence()
    {
        gameObject.SetActive(false);
    }
}
