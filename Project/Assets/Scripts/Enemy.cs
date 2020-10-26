using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy Stats
    public float healthPoints = 20;
    public float damage = 10;
    public float speed = 10;
    public bool attackMode = false;


    void Update()
    {
        if (healthPoints <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // How to react if hit by lazer
        if (other.CompareTag("Lazer"))
        {
            // Lose health
            healthPoints -= 10;

            // Destroy the lazer
            Destroy(other.gameObject);
        }
    }

    // TODO
    private void LookForPlayer()
    {
        
    }
}
