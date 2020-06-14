using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;         // range at which the enemy starts chasing the target
    [SerializeField] Animator animator;  
    [SerializeField] NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked;


    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;        
        }
    }

    // check how close to target, then chase or attack accordingly
    private void EngageTarget()
    {
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else if (distanceToTarget < navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    // move the enemy towards postiion of the target and start the animation for move - stop the attack if enemy was attacking prior
    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
        animator.SetTrigger("Move");
        animator.SetBool("Attack", false);
    }

    // attack the player - start attacking animation
    private void AttackTarget()
    {
        print("attack");
        animator.SetBool("Attack", true);
    }

    // Display the radius of enemy to be active when selected
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public void EnemyIsDead()
    {
        navMeshAgent.isStopped = true;
        animator.SetTrigger("Dead");
    }
}
