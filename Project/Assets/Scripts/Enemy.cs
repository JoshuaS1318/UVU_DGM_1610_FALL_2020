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

    public GameObject player;

    private float attackRange = 10;
    private float distanceToPlayer;


    void Update()
    {
        transform.LookAt(player.transform, Vector3.left);

        if (healthPoints <= 0)
        {
            Death();
        }

        // Search for the player
        LookForPlayer();

        // Attack the player if in range
        if (attackMode)
        {
            AttackPlayer();
        }

        AttackPlayer();
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

    private void LookForPlayer()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log(distanceToPlayer);

        if (distanceToPlayer < attackRange)
        {
            Debug.Log("Attack");
            attackMode = true;
        }
        else if (distanceToPlayer > attackRange * 2)
        {
            attackMode = false;
        }
    }

    private void AttackPlayer()
    {
        transform.LookAt(player.transform.position);
    }
}
