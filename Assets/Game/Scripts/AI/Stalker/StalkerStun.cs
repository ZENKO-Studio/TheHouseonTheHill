using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class StalkerStun : StalkerBaseState
{
    float currentTime = 0;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentTime = 0f;
        agent.speed = 0f;
        stalkerRef.stalkerAnimator.SetBool("Stun", true);

        if(stalkerRef.stalkerMaterial == null)
        {
            Debug.LogError($"{stalkerRef.name} didnt set the Material Property!");
        }
        else
        {
            // Enable emission keyword
            stalkerRef.stalkerMaterial.EnableKeyword("_EMISSION");

            // Set the emission color and intensity
            stalkerRef.stalkerMaterial.SetColor("_EmissiveColor", stalkerRef.emissionColor * stalkerRef.glowIntensity);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentTime += Time.deltaTime;

        float emission = Mathf.PingPong(Time.time, stalkerRef.glowIntensity);
        stalkerRef.stalkerMaterial.SetFloat("_EmissionIntensity", emission);

        if (currentTime > stalkerRef.stunTime) 
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
        stalkerRef.stalkerAnimator.SetBool("Stun", false);
        agent.speed = stalkerRef.moveSpeed;

        if (stalkerRef.stalkerMaterial != null)
        {
            stalkerRef.stalkerMaterial.SetFloat("_EmissionIntensity", 0f);
            stalkerRef.stalkerMaterial.DisableKeyword("_EMISSION");
        }
    }
}
