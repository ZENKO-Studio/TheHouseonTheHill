using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventBus;
using static UnityEngine.UI.Toggle;

public class InventoryUiController : MonoBehaviour
{
    public GameObject inventoryCanvas;

    private void OnEnable()
    {
        EventBus.Subscribe<ToggleInventoryEvent>(OnToggleInventory);
        inventoryCanvas.SetActive(false); // Ensure the inventory is initially hidden
    }

    bool isOpen = false;

    private void OnDisable()
    {
        EventBus.Unsubscribe<ToggleInventoryEvent>(OnToggleInventory);
    }

    private void OnToggleInventory(ToggleInventoryEvent toggleEvent)
    {
        inventoryCanvas.SetActive( toggleEvent.IsOpen );

    }

    #region Toggle Menu Pages
    //The 3 menu pages
    public List<GameObject> menuPages = new List<GameObject>();

    public void ResetMenuPages()
    {
        foreach (var page in menuPages)
        {
            page.SetActive(false);
        }
    }
    #endregion
}
