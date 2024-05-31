using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seq1 : SeqBase
{

    [SerializeField] GameObject stalkerPrefab;
    [SerializeField] Transform stalkerSpawnPos;
    public List<GameObject> wallsToDelete = new List<GameObject>();

    public override void TriggerSeq()
    {
        DeleteWalls();
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if(stalkerPrefab != null && stalkerSpawnPos != null)
        {
            Instantiate(stalkerPrefab, stalkerSpawnPos.position, stalkerSpawnPos.rotation);
            return;
        }
        Debug.Log($"Set the stalker prefab and its spawn position in {name} sequence!");
    }

    private void DeleteWalls()
    {
        if(wallsToDelete.Count == 0) 
        {
            Debug.Log($"{name} sequence have no walls to delete!");
            return;
        }

        foreach (var wall in wallsToDelete)
        {
            Destroy(wall);
        }
        wallsToDelete.Clear();
    }


}
