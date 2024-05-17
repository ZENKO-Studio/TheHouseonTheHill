using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerAttackState : StalkerBaseState
{
    float currentTime = 0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Do what needs to be done
        currentTime = 0f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stalkerRef.bPlayerSensed)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= stalkerRef.attackFrequency)
            {
                stalkerRef.Attack();
                currentTime = 0f;
            }

            if(Vector3.Distance(stalkerTransform.position, stalkerRef.playerTransform.position) > stalkerRef.attackRange)
            {
                fsm.ChangeState(StalkerFSM.ChasePlayerState);
            }
        }
        else
        {
            fsm.ChangeState(StalkerFSM.InvestigateState);
        }
    }
}
