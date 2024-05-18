using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public Transform keyItemContainer;
    public Transform resourceItemContainer;

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
    }

    public void RemoveItem(InventoryItem item)
    {
        items.Remove(item);
        Destroy(item.gameObject);
    }

    public void UseItem(InventoryItem item)
    {
        item.Use();
    }
}
