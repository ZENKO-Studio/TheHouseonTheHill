using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Interactable;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class OneSidedInteract : MonoBehaviour, IInteractable
{
    public UnityEvent onInteract;
    [SerializeField] private InputAction interactAction;
    [SerializeField] private int priority;

    public OpenThings openThings;

    private void Start()
    {
        openThings = GetComponentInParent<OpenThings>();
        if (openThings == null)
        {
            Debug.LogError("OpenThings component not found on the GameObject.");
        }
    }

    public InputAction Action => interactAction;

    public void Interact(CharacterBase player)
    {
        if (openThings != null && openThings.IsPlayerInFront(player.transform))
        {
          //e  Gizmos.DrawRay(player.transform.position , openThings.transform.position);
            onInteract?.Invoke();
        }
        else
        {
            Debug.Log("Player is not in front of the door.");
        }
    }

    public int Priority => priority;
}

