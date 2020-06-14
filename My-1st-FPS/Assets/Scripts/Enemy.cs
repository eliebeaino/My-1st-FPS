using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Import Assets")]
    [SerializeField] EnemyAI enemyAI;

    [Header("Enemy Stats")]
    public float enemyHealth = 100f;
    public float enemyDamage = 40f;
    public float deathTimer = 3f; // timer before corpse disappears after death

    // calculate new health after taking damage
    public void TakeDmg(float damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    // kill the enemy after some time , start death animation from enemyAI.cs
    private void Die()
    {
        Destroy(gameObject, deathTimer);
        enemyAI.EnemyIsDead();
        Destroy(this);
    }

    public void DamagePlayerEvent()
    {
        var player = enemyAI.target.GetComponent<Player>();
        if (player == null) return;
        player.TakeDmg(enemyDamage);
        // todo add visual queue for player damage
    }
}
