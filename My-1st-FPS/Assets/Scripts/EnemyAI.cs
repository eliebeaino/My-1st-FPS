using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Cameras;

public class EnemyAI : MonoBehaviour
{
    [Header("Import Assets")]
    public Transform target;
    [SerializeField] Animator animator;  
    [SerializeField] NavMeshAgent navMeshAgent;

    [Header("Enemy Propreties")]
    [SerializeField] float chaseRange = 5f;         // range at which the enemy starts chasing the target
    [SerializeField] float turnSpeed = 5f;          // enemy turn speed to face player
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked;
    bool isAlive = true;


    void Update()
    {
        if (isAlive)
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
    }

    // check how close to target, then chase or attack accordingly
    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else if (distanceToTarget < navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    // when enemy takes damage, triggers him to start engage target sequence
    public void OnDmgTaken()
    {
        isProvoked = true;
    }

    // move the enemy towards postiion of the target and start the animation for move - stop the attack if enemy was attacking prior
    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
        animator.SetTrigger("Move");
        animator.SetBool("Attack", false);
    }

    // start attacking animation
    private void AttackTarget()
    { 
        animator.SetBool("Attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    // Display the radius of enemy to be active when selected
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    // start Death Animation and stop the enemy movement - called once when enemy health reaches 0
    public void EnemyIsDead()
    {
        navMeshAgent.isStopped = true;
        animator.SetTrigger("Dead");
        isAlive = false;
    }
}
