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

        stalkerRef.stalkerAnimator.SetBool("Attack", true);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stalkerRef.bPlayerSensed)
        {
            //This part is replaced by making Attack an animation event
            //currentTime += Time.deltaTime;
            //if (currentTime >= stalkerRef.attackFrequency)
            //{
            //    stalkerRef.Attack();
            //    currentTime = 0f;
            //}

            if(!stalkerRef.CanAttackPlayer())
            {
                fsm.ChangeState(StalkerFSM.ChasePlayerState);
            }
        }
        else
        {
            fsm.ChangeState(StalkerFSM.InvestigateState);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        stalkerRef.stalkerAnimator.SetBool("Attack", false);
    }
}
