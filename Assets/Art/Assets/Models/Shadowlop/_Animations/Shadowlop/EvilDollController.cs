using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EvilDollController : MonoBehaviour
{
    public Transform target; // The target the enemy will move towards
    public NavMeshAgent agent;
    public Animator animator;

    void Start()
    {
        // Get the NavMeshAgent and Animator components
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Update the agent's destination
        if (target != null)
        {
            agent.SetDestination(target.position);
        }

        // Set blend tree parameter based on agent's speed
        animator.SetFloat("Speed", agent.velocity.magnitude);

        // Rotate the character to face the movement direction
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
