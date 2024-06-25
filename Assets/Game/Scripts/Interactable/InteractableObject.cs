using Game.Scripts.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractableObject : MonoBehaviour , IInteractable
{

    public UnityEvent onInteract;

    public bool _playerInRange; 

    public string ItemName;

    [SerializeField] private InputAction interactAction;
    [SerializeField] private int priority;

    public InputAction Action => interactAction;

    public int Priority => priority;

    public string GetItemName()
    {
        return ItemName;
    }

    public void Interact(CharacterBase player)
    {
        onInteract?.Invoke();
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }





}
