using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Object
{
    public int amount;

    public override void Use()
    {
        // Implement specific use logic for resource items
        Debug.Log("Using resource item: " + itemName);
        if (amount > 0)
        {
            amount--;
            Debug.Log("Remaining amount: " + amount);
        }
        else
        {
            Debug.Log("Out of " + itemName);
        }
    }
}
