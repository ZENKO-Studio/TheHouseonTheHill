/**@Sami 12/06/2024
 * 
 **/

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EventBus;

public class InventoryHandler : Singleton<InventoryHandler>
{
    [SerializeField] InventoryUiController inventoryUI;

    //Just for Debugging Purpose, Will be removed later
    public List<InventoryItem> items = new List<InventoryItem>();

    //4 Dictionaries with Item and Button (Since we have to add and remove both)
    public Dictionary<Photo, GameObject> photos; 
    public Dictionary<Document, GameObject> documents; 
    public Dictionary<Key, GameObject> keys; 
    public Dictionary<UsableObject, GameObject> usables; 

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
        items.Add(item);
        //CreateItemButton(item);

        EventBus.Publish(new ItemAddedEvent(item));
    }

    public void RemoveItem(InventoryItem item)
    {
        items.Remove(item);
        
        EventBus.Publish(new ItemRemovedEvent(item));
    }

    private void OnItemInspected(ItemInspectedEvent inspectedEvent)
    {
        Debug.Log("Item inspected: " + inspectedEvent.Item.itemName);
    }

    internal void AddUsable(UsableObject usableObject)
    {
        usables.Add(usableObject, inventoryUI.CreateItemButton(usableObject, InventoryPage.Usables));
    }

    internal void AddDocument(Document document)
    {
        documents.Add(document, inventoryUI.CreateItemButton(document, InventoryPage.Documents));
    }

    internal void AddPhoto(Photo photo)
    {
        photos.Add(photo, inventoryUI.CreateItemButton(photo, InventoryPage.Photos));
    }

    internal void AddKey(Key key)
    {
        keys.Add(key, inventoryUI.CreateItemButton(key, InventoryPage.Keys));
    }
}