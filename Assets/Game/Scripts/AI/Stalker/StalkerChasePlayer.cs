using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerChasePlayer : StalkerBaseState
{
    public float AngularDampeningTime = 5.0f;
    public float DeadZone = 10.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(stalkerRef.playerTransform.position);
        lastPlayerPos = stalkerRef.playerTransform.position;   

        agent.speed = stalkerRef.chaseSpeed;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!stalkerRef.bPlayerSensed)
        {
            fsm.ChangeState(StalkerFSM.InvestigateState);
        }

        //Recalculate Navmesh Path if player moved by certain distance
        if(Vector3.Distance(lastPlayerPos, agent.transform.position) > 2f)
        {
            agent.SetDestination(stalkerRef.playerTransform.position);
            lastPlayerPos = stalkerRef.playerTransform.position;
        }

        //#TODO: Maybe move this movement thing into Enemy Controller and Just Handle Destination Changes from here?
        //Actually Move the player
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
        
        if(agent.remainingDistance <= stalkerRef.attackRange) //#TODO: Change it to attack Range of Stalker
        {
            //Change State to Attack
            fsm.ChangeState(StalkerFSM.AttackState);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.speed = stalkerRef.moveSpeed;
    }

}
