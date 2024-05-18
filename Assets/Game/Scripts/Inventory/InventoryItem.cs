using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;

    public abstract void Use();
}
