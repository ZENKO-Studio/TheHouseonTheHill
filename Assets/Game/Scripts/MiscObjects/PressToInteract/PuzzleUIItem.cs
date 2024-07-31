using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleUIItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Canvas canvas;

    //Reference to the Collected Inventory Item
    public InventoryItem inventoryItem;
    RectTransform rectTransform;

    internal PuzzleUISlot assignedSlot = null;

    private bool bCanDrag = true;

    Vector2 initPos = Vector2.zero;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        if (canvas == null)
            print("Canvas Null");

        rectTransform = GetComponent<RectTransform>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        PuzzleUIController.itemBeingDragged = this;

        initPos = rectTransform.anchoredPosition;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta * canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (assignedSlot != null)
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
