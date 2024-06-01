using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CaptureInputHandler : MonoBehaviour
{
    [SerializeField] private GameObject keyItemPrefab; // Prefab for the key item
    [SerializeField] private Transform keyItemContainer; // Container to store key item objects
    public InputAction inputActions;

    private void Awake()
    {
      
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.performed += OnAddKeyItem;
    }

    private void OnDisable()
    {
        inputActions.performed -= OnAddKeyItem;
        inputActions.Disable();
    }

    private void OnAddKeyItem(InputAction.CallbackContext context)
    {
        AddKeyItem();
    }

    private void AddKeyItem()
    {
        // Instantiate the key item object and add it to the container
        if (keyItemPrefab != null && keyItemContainer != null)
        {
            GameObject newKeyItem = Instantiate(keyItemPrefab, keyItemContainer);
            // Optionally, set additional properties on the new key item here
        }
        else
        {
            Debug.LogWarning("Key Item Prefab or Container is not assigned.");
        }
    }
}
