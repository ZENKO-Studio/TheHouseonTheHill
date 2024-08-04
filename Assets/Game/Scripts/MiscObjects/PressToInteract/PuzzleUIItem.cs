using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleUIItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Canvas canvas;

    //Reference to the Collected Inventory Item
    public int no = 1;

    public InventoryItem inventoryItem;
    RectTransform rectTransform;

    Transform initialParent;

    internal PuzzleUISlot assignedSlot = null;

    private bool bCanDrag = true;

    Vector2 initPos = Vector2.zero;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        if (canvas == null)
            print("Canvas Null");

        rectTransform = GetComponent<RectTransform>();

        initialParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = canvas.transform;

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
            ResetPuzzlePiece();
        }

        PuzzleUIController.itemBeingDragged = null;

    }

    public void ResetPuzzlePiece()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        rectTransform.parent = initialParent;
        rectTransform.anchoredPosition = initPos;
    }
}
