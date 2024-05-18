using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KeyItem : Object
{
    public override void Use()
    {
        // Implement specific use logic for key items
        Debug.Log("Using key item: " + itemName);
    }
}

