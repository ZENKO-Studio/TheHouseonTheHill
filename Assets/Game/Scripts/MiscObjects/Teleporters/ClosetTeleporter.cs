using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetTeleporter : Teleporter
{
    [SerializeField] Light closetLight;
    [SerializeField] float lightToggleDelay = .5f;

    float curTime = 0f;
    bool bInteracting = false;

    protected override void Interact()
    {
        //To be changed to Input Event
        if(Input.GetKeyDown(KeyCode.E))
        {
            bInteracting = true;
        }

        if (bInteracting)
        {
            curTime += Time.deltaTime;
            if(curTime > lightToggleDelay) 
            {
                closetLight.enabled = false;
            }
            if(curTime > lightToggleDelay*2f)
            {
                Teleport();
                ResetTeleporter();
            }
        }
    }

    private void ResetTeleporter()
    {
        closetLight.enabled = true;
        curTime = 0f;
        bInteracting = false;
    }
}
