using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EventBus;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public Transform keyItemContainer;
    public Transform resourceItemContainer;
    public Transform contentTransform; // Reference to the ScrollView content

    public GameObject itemButtonPrefab; // Reference to the ItemButton prefab

    private void Start()
    {
        EventBus.Subscribe<ItemInspectedEvent>(OnItemInspected);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<ItemInspectedEvent>(OnItemInspected);
    }

    public void AddItem(InventoryItem item)
    {
        items.Add(item);
        if (item is KeyItem)
        {
            item.transform.SetParent(keyItemContainer);
        }
        else if (item is Resource)
        {
            item.transform.SetParent(resourceItemContainer);
        }
        CreateItemButton(item);

        EventBus.Publish(new ItemAddedEvent(item));
    }

    public void RemoveItem(InventoryItem item)
    {
        items.Remove(item);
        Destroy(item.gameObject);

        EventBus.Publish(new ItemRemovedEvent(item));
    }

    public void UseItem(InventoryItem item)
    {
        item.Use();
    }

    private void CreateItemButton(InventoryItem item)
    {
        GameObject buttonObject = Instantiate(itemButtonPrefab, contentTransform);
        Button button = buttonObject.GetComponent<Button>();
        button.onClick.AddListener(() => InspectItem(item));

        Text buttonText = buttonObject.GetComponentInChildren<Text>();
        buttonText.text = item.itemName;

        Image buttonImage = buttonObject.GetComponentsInChildren<Image>()[1];
        buttonImage.sprite = item.itemIcon;
    }

    private void InspectItem(InventoryItem item)
    {
        EventBus.Publish(new ItemInspectedEvent(item));
    }

    private void OnItemInspected(ItemInspectedEvent inspectedEvent)
    {
        // Handle item inspection logic if needed
        Debug.Log("Item inspected: " + inspectedEvent.Item.itemName);
    }
}
