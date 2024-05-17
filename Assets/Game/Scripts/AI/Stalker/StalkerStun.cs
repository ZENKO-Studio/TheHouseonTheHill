using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerStun : StalkerBaseState
{
    float currentTime = 0;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentTime = 0f;
        agent.speed = 0f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentTime += Time.deltaTime;

        if(currentTime > stalkerRef.stunTime) 
        {
            if (stalkerRef.bPlayerSensed)
            {
                fsm.ChangeState(StalkerFSM.ChasePlayerState);
            }
            else
            {
                fsm.ChangeState(StalkerFSM.PatrolState);
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.speed = stalkerRef.moveSpeed;
    }
}
