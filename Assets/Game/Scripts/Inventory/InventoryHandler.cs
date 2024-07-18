/**@Sami 12/06/2024
<<<<<<< HEAD
 * 
 **/

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
=======
 * Handles all the stuff related to inventory
 **/

using System.Collections.Generic;
using UnityEngine;
>>>>>>> Developing
using static EventBus;

public class InventoryHandler : Singleton<InventoryHandler>
{
    [SerializeField] InventoryUiController inventoryUI;

    //Just for Debugging Purpose, Will be removed later
    public List<InventoryItem> items = new List<InventoryItem>();

    //4 Dictionaries with Item and Button (Since we have to add and remove both)
<<<<<<< HEAD
    public Dictionary<Photo, GameObject> photos = new Dictionary<Photo, GameObject>(); 
    public Dictionary<Document, GameObject> documents = new Dictionary<Document, GameObject>(); 
    public Dictionary<Key, GameObject> keys = new Dictionary<Key, GameObject>(); 
    public Dictionary<UsableObject, GameObject> usables = new Dictionary<UsableObject, GameObject>(); 
=======
    public Dictionary<InventoryItem, GameObject> photos = new Dictionary<InventoryItem, GameObject>(); 
    public Dictionary<InventoryItem, GameObject> documents = new Dictionary<InventoryItem, GameObject>(); 
    public Dictionary<InventoryItem, GameObject> keys = new Dictionary<InventoryItem, GameObject>(); 
    public Dictionary<InventoryItem, GameObject> usables = new Dictionary<InventoryItem, GameObject>(); 
>>>>>>> Developing

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

<<<<<<< HEAD


    public void AddItem(InventoryItem item)
    {
        items.Add(item);
        //CreateItemButton(item);
=======
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
>>>>>>> Developing

        EventBus.Publish(new ItemAddedEvent(item));
    }

    public void RemoveItem(InventoryItem item)
    {
<<<<<<< HEAD
        items.Remove(item);
        
=======
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

>>>>>>> Developing
        EventBus.Publish(new ItemRemovedEvent(item));
    }

    private void OnItemInspected(ItemInspectedEvent inspectedEvent)
    {
        Debug.Log("Item inspected: " + inspectedEvent.Item.itemName);
    }

<<<<<<< HEAD
    internal void AddUsable(UsableObject usableObject)
    {
        print($"Adding {usableObject.name}");
        usables.Add(usableObject, inventoryUI.CreateItemButton(usableObject, InventoryPage.Usables));
        print(usables.ToString());
    }

    internal void AddDocument(Document document)
    {

        print($"Adding {document.name}");
        documents.Add(document, inventoryUI.CreateItemButton(document, InventoryPage.Documents));
        print(usables.ToString());
    }

    internal void AddPhoto(Photo photo)
    {

        print($"Adding {photo.name}");

        photos.Add(photo, inventoryUI.CreateItemButton(photo, InventoryPage.Photos));
        print(usables.ToString());
    }

    internal void AddKey(Key key)
    {
        print($"Adding {key.name}");
        keys.Add(key, inventoryUI.CreateItemButton(key, InventoryPage.Keys));
        print(keys.Count);
    }

=======
>>>>>>> Developing
    public bool HasKey(int id)
    {
        foreach (var key in keys) 
        {
<<<<<<< HEAD
            if(key.Key.keyID == id)
=======
            if(key.Key.itemId == id)
>>>>>>> Developing
                return true;
        }
        return false;
    }
}