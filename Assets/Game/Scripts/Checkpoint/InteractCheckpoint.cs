using Game.Scripts.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InteractCheckpoint : Checkpoint, IInteractable
{

    [SerializeField] private InputAction interactAction;
    [SerializeField] private int priority;
    public InputAction Action => interactAction;

    public int Priority => priority;

    public void Interact(CharacterBase player)
    {
        CheckPointSystem.Instance.SaveCheckpoint(this);
    }
}