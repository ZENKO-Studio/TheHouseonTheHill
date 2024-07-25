using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CinemaMachineSwitcher : MonoBehaviour
{
    [Header("Callbacks")]
    public UnityEvent onEnter;

    [Tooltip("If this is set to true it will automatically switch orientation even if player is moving")]
    [SerializeField] bool instantlySwitchOrientation = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterBase player))
        {
            OnEnter();
        }
    }

    private void OnEnter()
    {
        onEnter.Invoke();
        GameManager.Instance.playerRef.UpdateOrientation(instantlySwitchOrientation);
    }

}


