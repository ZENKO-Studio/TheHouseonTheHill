using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerInvestigate : StalkerBaseState
{
    public float AngularDampeningTime = 5.0f;
    public float DeadZone = 10.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(lastPlayerPos);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stalkerRef.bPlayerSensed)
        {
            fsm.ChangeState(StalkerFSM.ChasePlayerState);
        }

        if (stalkerRef.bSoundHeard)
        {
            fsm.ChangeState(StalkerFSM.InvestigateSoundState);
        }

        //#TODO: Maybe move this movement thing into Enemy Controller and Just Handle Destination Changes from here?
        //Actually Move the player
        if (agent.desiredVelocity != Vector3.zero)
        {

            float speed = Vector3.Project(agent.desiredVelocity, stalkerTransform.forward).magnitude * agent.speed;

            Vector3 nextPos = stalkerTransform.position + (stalkerTransform.forward * speed * Time.deltaTime);

            //stalkerTransform.position = nextPos;

            agent.velocity = (nextPos - agent.transform.position) / Time.deltaTime;

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

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            //Have some timer?
            fsm.ChangeState(StalkerFSM.PatrolState);
        }
    }
}
