using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BoardItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Canvas canvas;

    public InventoryItem inventoryItem;
    RectTransform rectTransform;

    //The button on the board system that Instantiated this thing
    public GameObject btnObject;

    internal Slot assignedSlot = null;

    private bool bCanDrag = true;

    Vector2 initPos = Vector2.zero;

    //The item in the inventory this represents
    //public GameObject representedItem; 

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        if (canvas == null)
            print("Canvas Null");
        
        rectTransform = GetComponent<RectTransform>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        TheBoardController.itemBeingDragged = this;

        initPos = rectTransform.anchoredPosition;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta * canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(assignedSlot != null)
        {
            assignedSlot.PlaceItem(this);
        }
        else
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            rectTransform.anchoredPosition = initPos;
        }

    }

    
}
