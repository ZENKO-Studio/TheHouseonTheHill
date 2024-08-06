using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CameraInteract : MonoBehaviour
{
    public UnityEvent onInteract;
    [SerializeField] private InputAction interactAction;
    [SerializeField] private int priority;
    [SerializeField] private Light flashLight; // Assign the light source in the inspector
    [SerializeField] private float flashDuration = 0.1f; // Duration of the flash
    [SerializeField] private GameObject wall; // Assign the wall to be destroyed

    public InputAction Action => interactAction;
    public int Priority => priority;

    private void Start()
    {
        interactAction.performed += ctx => Interact(null); // Adjust according to your input setup
        flashLight.enabled = false; // Ensure the flash is off at the start
    }

    public void Interact(CharacterBase player)
    {
        StartCoroutine(FlashCoroutine());
        onInteract?.Invoke();
        DestroyWall();
    }

    private IEnumerator FlashCoroutine()
    {
        flashLight.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        flashLight.enabled = false;
    }

    private void DestroyWall()
    {
        if (wall != null)
        {
            Destroy(wall);
        }
    }
}

