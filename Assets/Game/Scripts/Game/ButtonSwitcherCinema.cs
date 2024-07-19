using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ButtonSwitcherCinema : MonoBehaviour
{

    public UnityEvent onEnter;
    
    [SerializeField] private InputAction Action;

    private Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Action.Enable();
    }

    private void OnDisable()
    {
        Action.Disable();
    }

    private void Start()
    {
        Action.performed += _ => OnEnter();
    }

   

   private void OnEnter()
    {
        onEnter.Invoke();
        //Set this to false when you want to switch back to 3rd Person
        GameManager.Instance.playerRef.UpdateOrientation();
    }
}
