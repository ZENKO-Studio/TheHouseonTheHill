using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @Sami 06/12/24
/// This is the class for any usable object like Gloves / Etc
/// </summary>
/// 
public class UsableObject : InventoryItem
{
    public override string GetDescription()
    {
        return itemDescription;
    }

    public override void Interact()
    {
        if(inventoryHandler != null) 
        {
            inventoryHandler.AddUsable(this);
        }
    }

    public void Use()
    {
        // Implement general use logic for object items
        Debug.Log("Using object item: " + itemName);
    }
}
