using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public bool bEnableMovement = true;

    CharacterController characterController;

    [SerializeField] float movSpeed = 5f;

    #region Input Values
    public Vector2 moveInput;
    public Vector2 lookInput;
    public bool jump;
    public bool sprint;

    public bool cursorLocked = true;
		public bool cursorInputForLook = true;
    #endregion

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
 }

    private void Update()
    {
        if (characterController != null)
        {
            PlayerMovement();
        }
    }

    private void PlayerMovement()
    {
        if (!bEnableMovement)
            return;

        Vector3 movDir = new Vector3(moveInput.x, 0, moveInput.y);
        movDir.Normalize();

        characterController.SimpleMove(movDir * movSpeed);



    }

    #region Read Inputs
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            lookInput = value.Get<Vector2>();
        }
    }

    public void OnJump(InputValue value)
    {
        jump = value.isPressed;
    }

    public void OnSprint(InputValue value)
    {
        sprint = value.isPressed;
    }

    public void OnInteract(InputValue value)
    {
        Debug.Log($"{name} is Interacting");
    }
    #endregion
}
