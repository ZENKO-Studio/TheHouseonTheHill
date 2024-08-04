using UnityEngine;

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


    public bool bInteractable = true; //Make it false when already interacted with

    protected InventoryHandler inventoryHandler;    //Just a local reference of Inventory System (just to avoid writing the whole thing)

    #region Glowing Part

    [SerializeField] bool bShouldGlow = false;

    public Material objectMaterial;

    public Color emissionColor = Color.yellow;
    public float maxGlowIntensity = 50.0f;
    public AnimationCurve intensityMultiplier;
    #endregion

    protected virtual void Start()
    {
        inventoryHandler = InventoryHandler.Instance;
        GetComponent<Collider>().isTrigger = true;

        if(bShouldGlow)
        {
            objectMaterial = GetComponentInChildren<Renderer>().material;
            Debug.Log($"{objectMaterial.name}");
        }



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
            GameManager.Instance.playerRef.SetInventoryItem(this);
            if (interactPopup != null)
            {
                interactPopup.SetActive(true);
            }
            if(bShouldGlow)
            {
                // Enable emission keyword
                objectMaterial.EnableKeyword("_EMISSION");

                // Set the emission color and intensity
                objectMaterial.SetColor("_EmissiveColor", emissionColor * intensityMultiplier.Evaluate(0));
            }
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && bShouldGlow)
        {
            float emission = maxGlowIntensity * intensityMultiplier.Evaluate(Time.time % 1);

            // Set the emission color and intensity
            objectMaterial.SetColor("_EmissiveColor", emissionColor * emission);

        }

    }
    protected void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            GameManager.Instance.playerRef.RemoveInventoryItem(this);
            if (interactPopup != null)
            {
                interactPopup.SetActive(false);
            }
            if (bShouldGlow)
            {
                // Set the emission color and intensity
                objectMaterial.SetColor("_EmissiveColor", emissionColor * intensityMultiplier.Evaluate(0));
                
                // Disable emission keyword
                objectMaterial.DisableKeyword("_EMISSION");

            }
        }
    }

    public virtual void Interact()
    {
        inventoryHandler.AddItem(this);
        GameManager.Instance.playerHud.UpdateDialogueText($"{itemName} added to inventory");

        PostInteract();
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
