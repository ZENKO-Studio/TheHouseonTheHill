using Game.Scripts.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SphereCollider))]
public abstract class InventoryItem : MonoBehaviour
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
        GetComponent<Collider>().isTrigger = true;

        //No triggers for stuff that is not interactable
        if(!bInteractable) 
        { 
            GetComponent<Collider>().enabled = false;
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.playerRef.SetInteractable(this);
            if (interactPopup != null)
            {
                interactPopup.SetActive(true);
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            GameManager.Instance.playerRef.RemoveInteractable(this);
            if (interactPopup != null)
            {
                interactPopup.SetActive(false);
            }
        }
    }

    public virtual void Interact()
    {
       
    }

    //Disable Interactable after interacting
    protected void PostInteract()
    {
        bInteractable = false;
        if (interactPopup != null)
        {
            interactPopup.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
