using Game.Scripts.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItem : MonoBehaviour
{
    public string itemName; //Name of Item
    public Sprite itemIcon; //Icon to show on button
    public GameObject itemPreview; //Mesh to be shown on Inventory Cam
    public string itemDescription; //Mesh to be shown on Inventory Cam

    [HideInInspector] public int btnIndex;    //Index of Inventory Button (To delete it from Inventory UI)

    protected InventoryHandler inventoryHandler;
    
    //public ItemType itemType;

    private void Start()
    {
        inventoryHandler = InventoryHandler.Instance;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.playerRef.SetInteractable(this);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.playerRef.SetInteractable(null);
        }
    }

    public abstract void Interact();

    public abstract string GetDescription();
}
