using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class EvilDollController : MonoBehaviour
{
    public Transform target; // The target the enemy will move towards
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        // Get the NavMeshAgent and Animator components
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        // Ensure the NavMeshAgent does not control movement
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    void Update()
    {
        // Update the agent's destination
        if (target != null)
        {
            agent.SetDestination(target.position);
        }

        // Get the desired velocity in local space
        Vector3 localDesiredVelocity = transform.InverseTransformDirection(agent.desiredVelocity);

        // Set blend tree parameters
        animator.SetFloat("Speed", localDesiredVelocity.z);
        animator.SetFloat("Turn", localDesiredVelocity.x);

        // Move the agent using root motion
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            agent.nextPosition = transform.position;
        }
        else
        {
            animator.SetFloat("Speed", 0);
            animator.SetFloat("Turn", 0);
        }
    }

    void OnAnimatorMove()
    {
        // Apply root motion
        transform.position = animator.rootPosition;
        transform.rotation = animator.rootRotation;
    }
}
