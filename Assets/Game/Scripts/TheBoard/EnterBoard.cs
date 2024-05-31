using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnterBoard : MonoBehaviour
{
    public LayerMask interactableLayer; // Layer for interactable objects
    public float interactDistance = 2.0f; // Distance for interaction

    public Camera boardCamera; // Reference to the board camera

    public InputAction playerInputActions;
    private bool isBoardCameraActive = false;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;

    }

    private void OnEnable()
    {
        playerInputActions.Enable();
        playerInputActions.performed += OnInteract;
    }

    private void OnDisable()
    {
      
        playerInputActions.performed -= OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (isBoardCameraActive)
        {
            // Logic to turn off board camera and turn on main camera
            boardCamera.enabled = false;
            mainCamera.enabled = true;
            isBoardCameraActive = false;
        }
        else
        {
            TryInteractWithBoard();
        }
    }

    private void TryInteractWithBoard()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            if (hit.collider.CompareTag("TheBoard"))
            {
                // Logic to turn off main camera and activate board camera automatically
                mainCamera.enabled = false;
                boardCamera.enabled = true;
                isBoardCameraActive = true;
            }
        }
    }

}
