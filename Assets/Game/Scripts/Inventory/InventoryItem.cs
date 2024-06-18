using Game.Scripts.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItem : MonoBehaviour, IInteractable
{
    public string itemName; //Name of Item
    public Sprite itemIcon; //Icon to show on button
    public GameObject itemPreview; //Mesh to be shown on Inventory Cam
    public string itemDescription; //Mesh to be shown on Inventory Cam
    public GameObject interactPopup; //Popup to show when Player is Close
    //public string interactDescription;    //Can be enabled to have dynamic messages (As of now assuming to be in Prefab UI Itself)


    public bool bInteractable = true; //Make it false when already interacted with

    protected InventoryHandler inventoryHandler;    //Just a local reference of Inventory System (just to avoid writing the whole thing)
    
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

    public virtual void Interact(CharacterBase player)
    {
        //Default Base Method (Will be overrided in other sub classes)
    }
}
