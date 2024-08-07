/**@Sami 12/06/2024
 * Handles all the stuff related to inventory
 **/

using System;
using System.Collections.Generic;
using UnityEngine;
using static EventBus;

public class InventoryHandler : Singleton<InventoryHandler>
{
    [SerializeField] InventoryUiController inventoryUI;

    //Just for Debugging Purpose, Will be removed later
    public List<InventoryItem> items = new List<InventoryItem>();

    //4 Dictionaries with Item and Button (Since we have to add and remove both)
    public Dictionary<InventoryItem, GameObject> photos = new Dictionary<InventoryItem, GameObject>(); 
    public Dictionary<InventoryItem, GameObject> documents = new Dictionary<InventoryItem, GameObject>(); 
    public Dictionary<InventoryItem, GameObject> keys = new Dictionary<InventoryItem, GameObject>(); 
    public Dictionary<InventoryItem, GameObject> usables = new Dictionary<InventoryItem, GameObject>(); 

    private void OnEnable()
    {
        // Initialize the inventory with existing items
        foreach (var item in items)
        {
            //CreateItemButton(item);
        }

        //EventBus.Subscribe<ItemInspectedEvent>(OnItemInspected);
        //EventBus.Subscribe<ItemAddedEvent>(OnItemAdded);
        //EventBus.Subscribe<ItemRemovedEvent>(OnItemRemoved);
    }

    private void OnDisable()
    {
        //EventBus.Unsubscribe<ItemInspectedEvent>(OnItemInspected);
        //EventBus.Unsubscribe<ItemAddedEvent>(OnItemAdded);
        //EventBus.Unsubscribe<ItemRemovedEvent>(OnItemRemoved);
    }

    public void AddItem(InventoryItem item)
    {
        //Depending on Item Type Add to respective dictionary
        switch(item.itemType)
        {
            case ItemType.UsableObj:
                print($"Adding {item.name}");
                usables.Add(item, inventoryUI.CreateItemButton(item, InventoryPage.Usables));
                break; 
            case ItemType.Key:
                print($"Adding {item.name}");
                keys.Add(item, inventoryUI.CreateItemButton(item, InventoryPage.Keys));
                break; 
            case ItemType.Document:
                print($"Adding {item.name}");
                documents.Add(item, inventoryUI.CreateItemButton(item, InventoryPage.Documents));
                break; 
            case ItemType.Photo:
                print($"Adding {item.name}");
                photos.Add(item, inventoryUI.CreateItemButton(item, InventoryPage.Photos));
                break;
            default:
                Debug.Log("Something went wrong...");
                break;
        }

        EventBus.Publish(new ItemAddedEvent(item));
    }

    public void RemoveItem(InventoryItem item)
    {
        GameObject g = null;
        //Depending on Item Type Add to respective dictionary
        switch (item.itemType)
        {
            case ItemType.UsableObj:
                print($"Deleting {item.name}");
                g = usables[item];
                usables.Remove(item);
                if (g != null)
                    Destroy(g);
                break;
            case ItemType.Key:
                print($"Deleting {item.name}");
                g = keys[item];
                keys.Remove(item);
                if (g != null)
                    Destroy(g);
                break;
            case ItemType.Document:
                print($"Deleting {item.name}");
                g = documents[item];
                documents.Remove(item);
                if (g != null)
                    Destroy(g);
                break;
            case ItemType.Photo:
                print($"Deleting { item.name}");
                g = photos[item];
                photos.Remove(item);
                if (g != null)
                    Destroy(g); 
                break;
            default:
                Debug.Log("Something went wrong...");
                break;
        }

        EventBus.Publish(new ItemRemovedEvent(item));
    }

    private void OnItemInspected(ItemInspectedEvent inspectedEvent)
    {
        Debug.Log("Item inspected: " + inspectedEvent.Item.itemName);
    }

    public bool HasKey(int id)
    {
        foreach (var key in keys) 
        {
            if(key.Key.itemId == id)
                return true;
        }
        return false;
    }

    internal bool HasUsableItem(int itemId)
    {
        foreach (var usable in usables)
        {
            if (usable.Key.itemId == itemId)
                return true;
        }
        return false;
    }
}