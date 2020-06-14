using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;         // range at which the enemy starts chasing the target
    
    NavMeshAgent NavMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked;

    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

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
        if (distanceToTarget >= NavMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else if (distanceToTarget < NavMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    // move the enemy towards postiion of the target
    private void ChaseTarget()
    {
        NavMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        print("attack");
    }

    // Display the radius of enemy to be active when selected
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
