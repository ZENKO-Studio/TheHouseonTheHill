using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Interactable;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ButtonSwitcherCinema : MonoBehaviour , IInteractable
{

    public UnityEvent onEnter;

    public UnityEvent onInteract;

    private Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        
    }

  
   private void OnEnter()
    {
        onEnter.Invoke();
        //Set this to false when you want to switch back to 3rd Person
        GameManager.Instance.playerRef.UpdateOrientation();
    }

    [SerializeField] private InputAction interactAction;
    [SerializeField] private int priority;

    public InputAction Action => interactAction;
    public void Interact(CharacterBase player)
    {
        onInteract?.Invoke();
    }
    public int Priority => priority;}
