using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private bool initialSavePoint = false;

    private BoxCollider _trigger;

    private void Start()
    {
        if (TryGetComponent(out _trigger))
        {
            _trigger.isTrigger = true;
        }

        if (initialSavePoint)
        {
            Debug.Log($"Setting {name} as initial Checkpoint.");
            CheckPointSystem.Instance.SaveCheckpoint(this);
        }
    }

    private void OnValidate()
    {
        _trigger = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterBase _))
        {
            CheckPointSystem.Instance.SaveCheckpoint(this);
        }
    }

    public void Spawn(NellController player)
    {
        Debug.Log($"Spawning {player} at {name}");
        player.characterController.enabled = false;
        player.transform.SetPositionAndRotation(spawnPosition.position, Quaternion.identity);
        player.characterController.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnPosition.position, 0.04f);
    }
}