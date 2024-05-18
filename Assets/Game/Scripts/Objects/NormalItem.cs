using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NormalItem : Object
{
    public override void Use()
    {
        // Implement general use logic for object items
        Debug.Log("Using object item: " + itemName);
    }
}
