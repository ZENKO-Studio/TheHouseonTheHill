using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventBus;

public class InventoryUiController : MonoBehaviour
{
    public Canvas inventoryCanvas;

    private void Start()
    {
        EventBus.Subscribe<ToggleInventoryEvent>(OnToggleInventory);
        inventoryCanvas.enabled = false; // Ensure the inventory is initially hidden
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<ToggleInventoryEvent>(OnToggleInventory);
    }

    private void OnToggleInventory(ToggleInventoryEvent toggleEvent)
    {
        inventoryCanvas.enabled = toggleEvent.IsOpen;
    }
}
