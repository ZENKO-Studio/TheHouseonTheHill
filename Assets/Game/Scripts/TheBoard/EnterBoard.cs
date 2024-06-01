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
    public InputAction interactActionReference; // Reference to the input action for interaction

    private bool isBoardCameraActive = false;
    public Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        interactActionReference.Enable();
        interactActionReference.performed += OnInteract;
    }

    private void OnDisable()
    {
        interactActionReference.performed -= OnInteract;
        interactActionReference.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (isBoardCameraActive)
        {
            // Switch back to the main camera
            SwitchToMainCamera();
        }
        else
        {
            // Try to interact with the board
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
                // Switch to the board camera
                SwitchToBoardCamera();
            }
        }
    }

    private void SwitchToMainCamera()
    {
        mainCamera.enabled = true;
        boardCamera.enabled = false;
        isBoardCameraActive = false;
    }

    private void SwitchToBoardCamera()
    {
        mainCamera.enabled = false;
        boardCamera.enabled = true;
        isBoardCameraActive = true;
    }
}
