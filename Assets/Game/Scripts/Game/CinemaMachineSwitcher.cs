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
    
    [Tooltip("Uncheck this if you want the transition animation to not play on this very trigger")]
    [SerializeField] bool playTransitionAnimation = true; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterBase player))
        {
            OnEnter();
        }
    }

    private void OnEnter()
    {
        if(playTransitionAnimation)
        {
            GameManager.Instance.playerHud.PlayBlinkAnim(this);
        }
        else
        {
            Blink();
        }
    }

    //Called when eyes closed from animation
    public void Blink()
    {
        onEnter.Invoke();
        GameManager.Instance.playerRef.UpdateOrientation(instantlySwitchOrientation);
    }
}


