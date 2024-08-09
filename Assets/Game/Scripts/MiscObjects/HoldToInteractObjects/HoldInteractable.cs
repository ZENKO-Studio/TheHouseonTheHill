using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldInteractable : MonoBehaviour
{
    protected NellController playerRef;

    [Tooltip("How long the button should be held in orde to successfully interact")]
    protected float interactedTime = 0f;

    [SerializeField] protected GameObject interactablePopup;

    [SerializeField] protected float interactDuration = 5f;

    [SerializeField] protected Slider sliderRef;

    [SerializeField] protected float resetSpeed = .1f;
 
    protected bool bInteractionComplete = false;

    protected virtual void Start()
    {
        interactablePopup.SetActive(false);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if(!bInteractionComplete)
        {
            other.gameObject.TryGetComponent<NellController>(out playerRef);
            interactablePopup.SetActive(true);
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (playerRef && !bInteractionComplete)
        {
            
            if(playerRef.bInteracting && sliderRef != null)
            {
                interactedTime += Time.deltaTime;
                sliderRef.value = interactedTime / interactDuration;
            }
            else if(sliderRef != null)
            {
                interactedTime = 0;
                sliderRef.value = sliderRef.value <= 0 ? 0 : sliderRef.value - resetSpeed;
            }

            if (interactedTime >= interactDuration)
            {
                OnInteractionComplete();
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        interactedTime = 0f;
        sliderRef.value = interactedTime;
        interactablePopup?.SetActive(false);
        playerRef = null;
    }

    //Override this method to make different hold interactables doing different stuff
    //"CALL THE BASE METHOD" TO CLEAN UP THE STUFF "AFTER" ACTUAL IMPLEMENTATION
    protected virtual void OnInteractionComplete()
    {
        bInteractionComplete = true;
        interactablePopup?.SetActive(false);
        playerRef = null;
    }
}
