using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerPatrol : StalkerBaseState
{
    Transform currentDest;

    public float AngularDampeningTime = 5.0f;
    public float DeadZone = 10.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentDest = stalkerRef.GetNextWaypoint();
        if (currentDest != null)
        {
            agent.SetDestination(currentDest.position);
        }
        else
        {
            Debug.LogWarning("No next waypoint found");
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stalkerRef.bPlayerSensed)
        {
            fsm.ChangeState(StalkerFSM.ChasePlayerState);
            return;
        }

        if (stalkerRef.bSoundHeard)
        {
            fsm.ChangeState(StalkerFSM.InvestigateSoundState);
            return;
        }

        if (agent.desiredVelocity != Vector3.zero)
        {
            float speed = Vector3.Project(agent.desiredVelocity, stalkerTransform.forward).magnitude * agent.speed;

            Vector3 nextPos = stalkerTransform.position + (stalkerTransform.forward * speed * Time.deltaTime);

            Vector3 displacement = nextPos - agent.transform.position;

            if (displacement != Vector3.zero)
            {
                agent.velocity = displacement / Time.deltaTime;
            }
            else
            {
                agent.velocity = Vector3.zero;
            }

            float angle = Vector3.Angle(stalkerTransform.forward, agent.desiredVelocity);

            if (Mathf.Abs(angle) <= DeadZone)
            {
                stalkerTransform.LookAt(stalkerTransform.position + agent.desiredVelocity);
            }
            else
            {
                stalkerTransform.rotation = Quaternion.Lerp(stalkerTransform.rotation,
                                                     Quaternion.LookRotation(agent.desiredVelocity),
                                                     Time.deltaTime * AngularDampeningTime);
            }
        }
        else
        {
            agent.velocity = Vector3.zero;
        }

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            currentDest = stalkerRef.GetNextWaypoint();
            if (currentDest != null)
            {
                agent.SetDestination(currentDest.position);
            }
            else
            {
                Debug.LogWarning("No next waypoint found");
            }
        }
    }
}