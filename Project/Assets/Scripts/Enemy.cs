using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : WorldObject
{
    // Enemy Stats
    public float healthPoints = 20;
    public float damage = 10;
    public float speed = 4;
    public bool attackMode = false;

    public GameObject lazerPrefab;

    private GameObject player;

    // Weapons variables
    private float attackRange = 10;
    private float attackCooldown = 5;
    private bool canAttack = true;

    // Following variables
    private float distanceToPlayer;
    private float followingDistance = 10;


    private void Start()
    {
        // Get a reference to the player
        player = GameObject.Find("Spaceship");
    }

    void Update()
    {

        // If the enemy runs out of health kill it
        if (healthPoints <= 0)
        {
            DeathSequence();
        }

        // Search for the player
        LookForPlayer();

        // Attack the player if in range
        if (attackMode)
        {
            AttackPlayer();
        }
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

        // If the player is in the enemies range set mode to attack
        if (distanceToPlayer < attackRange)
        {
            attackMode = true;
        }
        // If the player is farther than double attack range stop attacking it
        else if (distanceToPlayer > attackRange * 3)
        {
            attackMode = false;
        }
    }

    private void AttackPlayer()
    {
        // Look at the player
        transform.LookAt(player.transform.position);

        // Move to attack range
        if (distanceToPlayer > followingDistance)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        // attack if not on cooldown
        if (canAttack)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        // Set the weapons to false
        canAttack = false;

        // Wait for the attack cooldown
        yield return new WaitForSeconds(attackCooldown);

        // Set canAttack back to true and attack if the player is still in range
        if (distanceToPlayer < attackRange)
        {
            Instantiate(lazerPrefab, transform.position, transform.rotation);
        }
        canAttack = true;
    }
}
