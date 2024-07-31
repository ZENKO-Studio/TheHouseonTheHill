using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CinemaMachineSwitcher : MonoBehaviour
{
    [Header("Callbacks")]
    public UnityEvent onEnter;

    [SerializeField]
    internal bool bTriggerable = true;

    [Tooltip("This is the reference to counterpart trigger (e.g. switch back trigger of one camera and vice versa)")]
    [SerializeField] CinemaMachineSwitcher relatedTrigger;

    [Tooltip("If this is set to true it will automatically switch orientation even if player is moving")]
    [SerializeField] bool instantlySwitchOrientation = false; 
    
    [Tooltip("Uncheck this if you want the transition animation to not play on this very trigger")]
    [SerializeField] bool playTransitionAnimation = true;

    [SerializeField] public bool avalibleRelatedTrigger = true;


    private void Start()
    {
      //  relatedTrigger = relatedTrigger.GetComponent<CinemaMachineSwitcher>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && relatedTrigger == null)
        {
            bTriggerable = false;
            OnEnter();
            

        }

        //if(relatedTrigger == null)return;
        
        if(other.CompareTag("Player") && bTriggerable)
        {
            bTriggerable = false;
            relatedTrigger.bTriggerable = true;
            Debug.Log("PlayerDetected");
            OnEnter();
        }
    }

    private void OnEnter()
    {
        if(playTransitionAnimation)
        {
            Debug.Log("PlayingTransitionAnim");
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
        GameManager.Instance.playerRef.UpdateOrientation();
    }
}


