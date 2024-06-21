// Alvin Philips
// June 21th, 2024
// Checkpoint System.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPointSystem : Singleton<CheckPointSystem>
{
    [SerializeField] private NellController player;
    [SerializeField] private SaveCheckPointIcon savingIcon;

    private Checkpoint _lastCheckpoint;

    public void SaveCheckpoint(Checkpoint savePoint)
    {
        ForceGrabValues();
        if (savingIcon)
        {
            savingIcon.Play();
        }
        _lastCheckpoint = savePoint;
    }

    public void ForceGrabValues()
    {
        // TODO: Really bad, @alvin fix ASAP
        player = FindObjectOfType<NellController>();
        savingIcon = FindObjectOfType<SaveCheckPointIcon>();
    }


    public void Respawn()
    {
        ForceGrabValues();
        Debug.Log("Respawning Players");
        if (_lastCheckpoint)
        {
            _lastCheckpoint.Spawn(player);
        }
    }
}

