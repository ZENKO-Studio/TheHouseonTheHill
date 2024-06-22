using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillFloor : MonoBehaviour
{
    public UnityEvent onKill;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out NellController _))
        {
            CheckPointSystem.Instance.Respawn();
            onKill?.Invoke();
        }
    }
}

