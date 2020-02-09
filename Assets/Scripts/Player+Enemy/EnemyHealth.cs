using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;          // The amount of health the enemy starts the game with.
    public int currentHealth;                    // The current health the enemy has.

    ParticleSystem hitParticles;             // Reference to the particle system that plays when the enemy is damaged.
  
    bool isDead;                                  // Whether the enemy is dead.

    void Awake()
    {
        hitParticles = GetComponentInChildren<ParticleSystem>();
        // Setting the current health when the enemy first spawns.
        currentHealth = maxHealth;
    }


    public void TakeDamage(int amount)
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;


        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;

        // And play the particles.
        hitParticles.Play();

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        GameObject.Destroy(this.gameObject);

    }
}
