<<<<<<< HEAD
using Game.Scripts.Interactable;
using System.Collections;
using System.Collections.Generic;
=======
>>>>>>> Developing
using UnityEngine;
using UnityEngine.InputSystem;

<<<<<<< HEAD
[RequireComponent(typeof(SphereCollider))]
public abstract class InventoryItem : MonoBehaviour
{
    public string itemName; //Name of Item
    public Sprite itemIcon; //Icon to show on button
    public GameObject itemPreview; //Mesh to be shown on Inventory Cam
    public string itemDescription; //Mesh to be shown on Inventory Cam
    public GameObject interactPopup; //Popup to show when Player is Close
    //public string interactDescription;    //Can be enabled to have dynamic messages (As of now assuming to be in Prefab UI Itself)


=======
public enum ItemType
{
    UsableObj,
    Key,
    Document,
    Photo
}

[RequireComponent(typeof(SphereCollider))]
public class InventoryItem : MonoBehaviour
{
    public string itemName; //Name of Item
    public Sprite itemIcon; //Icon to show on button
    public string itemDescription; //Description of the item

    public GameObject itemPreview; //Mesh to be shown on Inventory Cam (reference to prefab so that it can be instantiated)
    public GameObject boardItemPreview; //Representation in the Board (Can be an sized up image / item preview) depending on the actual board system

    public GameObject interactPopup; //Popup to show when Player is Close
    //public string interactDescription;    //Can be enabled to have dynamic messages (As of now assuming to be in Prefab UI Itself)

    public int itemId = -1; //To distinguish Item (Like Key ID / Document Id)
    public ItemType itemType;


>>>>>>> Developing
    public bool bInteractable = true; //Make it false when already interacted with

    protected InventoryHandler inventoryHandler;    //Just a local reference of Inventory System (just to avoid writing the whole thing)
   
<<<<<<< HEAD
    private void Start()
=======
    protected virtual void Start()
>>>>>>> Developing
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
<<<<<<< HEAD
            GameManager.Instance.playerRef.SetInteractable(this);
=======
            GameManager.Instance.playerRef.SetInventoryItem(this);
>>>>>>> Developing
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
<<<<<<< HEAD
            GameManager.Instance.playerRef.RemoveInteractable(this);
=======
            GameManager.Instance.playerRef.RemoveInventoryItem(this);
>>>>>>> Developing
            if (interactPopup != null)
            {
                interactPopup.SetActive(false);
            }
        }
    }

    public virtual void Interact()
    {
<<<<<<< HEAD
       
=======
        inventoryHandler.AddItem(this);

        PostInteract();
>>>>>>> Developing
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
