using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerHealth = 100f;

    // calculate new health after taking damage
    public void TakeDmg(float damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    // kill the player
    private void Die()
    {
        GetComponent<DeathHandler>().HandleDeath();
    }
}
