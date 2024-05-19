using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Inventory inventory;
    public GameObject keyItemPrefab;
    public GameObject resourceItemPrefab;

    private void Start()
    {
        // Add some items to the inventory for testing
        AddKeyItem("Golden Key");
        AddResourceItem("Salt", 10);
    }

    public void AddKeyItem(string name)
    {
        GameObject keyItemObject = Instantiate(keyItemPrefab);
        KeyItem keyItem = keyItemObject.GetComponent<KeyItem>();
        keyItem.itemName = name;
        inventory.AddItem(keyItem);
    }

    public void AddResourceItem(string name, int amount)
    {
        GameObject resourceItemObject = Instantiate(resourceItemPrefab);
        Resource resourceItem = resourceItemObject.GetComponent<Resource>();
        resourceItem.itemName = name;
        resourceItem.amount = amount;
        inventory.AddItem(resourceItem);
    }
}
