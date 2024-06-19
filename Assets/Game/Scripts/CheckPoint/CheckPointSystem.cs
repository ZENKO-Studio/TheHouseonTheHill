using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPointSystem : Singleton<CheckPointSystem>
{
    [SerializeField] private List<NellController> players;
    [SerializeField] private SaveCheckPointIcon savingIcon;

    private Checkpoint _lastCheckpoint;
    private bool _shouldSpawn;

    public void SaveCheckpoint(Checkpoint savePoint)
    {
        ForceGrabValues();
        savingIcon.Play();
        _lastCheckpoint = savePoint;
    }

    public void ForceGrabValues()
    {
        // TODO: Really bad, @alvin fix ASAP
        players = FindObjectsOfType<NellController>().ToList();
        savingIcon = FindObjectOfType<SaveCheckPointIcon>();
    }


    public void Respawn()
    {
        ForceGrabValues();
        Debug.Log("Respawning Players");
        _shouldSpawn = true;
    }

    public void FixedUpdate()
    {
        if (!_shouldSpawn) return;

        _lastCheckpoint.Spawn(players);
        _shouldSpawn = false;
    }
}

