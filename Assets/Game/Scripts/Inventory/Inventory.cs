using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    // References to the inspection UI elements
    public TMP_Text itemNameText;
    public Image itemIconImage;
    public TMP_Text itemDescriptionText;
    public Transform itemPreviewParent;

    private void Start()
    {
        EventBus.Subscribe<ItemInspectedEvent>(OnItemInspected);

        foreach (var item in items)
            CreateItemButton(item);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<ItemInspectedEvent>(OnItemInspected);
    }

    public void AddItem(InventoryItem item)
    {
        items.Add(item);
       
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
        GameObject buttonObject = Instantiate(itemButtonPrefab);
        if (item is KeyItem)
        {
           buttonObject.transform.SetParent(keyItemContainer, false);
        }
        else if (item is Resource)
        {
            buttonObject.transform.SetParent(resourceItemContainer, false);
        }
        item.btnIndex = buttonObject.transform.GetSiblingIndex();
        Button button = buttonObject.GetComponent<Button>();
        button.onClick.AddListener(() => InspectItem(item));

        Text buttonText = buttonObject.GetComponentInChildren<Text>();
        //buttonText.text = item.itemName;

        Image buttonImage = buttonObject.GetComponentInChildren<Image>();
        buttonImage.sprite = item.itemIcon;
    }

    private void RemoveItemButton(InventoryItem item)
    {
        if (item is KeyItem)
        {
            Destroy(keyItemContainer.transform.GetChild(item.btnIndex));    
        }
        else if (item is Resource)
        {
            Destroy(keyItemContainer.transform.GetChild(item.btnIndex));
        }
    }

    private void InspectItem(InventoryItem item)
    {
        //Remove previously previeved items from preview
        for (int i = 0; i < itemPreviewParent.childCount; i++)
        {
            if(itemPreviewParent.GetChild(i) != itemPreviewParent)
                Destroy(itemPreviewParent.GetChild(i).gameObject);
        }

        EventBus.Publish(new ItemInspectedEvent(item));
        // Display item details in the inspection panel
        itemNameText.text = item.itemName;
        //itemIconImage.sprite = item.itemIcon;
        itemDescriptionText.text = item.GetDescription(); // Assuming GetDescription() returns item details
        GameObject previewOb = Instantiate(item.itemPreview, new Vector3(0, 0, 0), Quaternion.identity, itemPreviewParent);
    }

    private void OnItemInspected(ItemInspectedEvent inspectedEvent)
    {
        Debug.Log("Item inspected: " + inspectedEvent.Item.itemName);
    }
}
