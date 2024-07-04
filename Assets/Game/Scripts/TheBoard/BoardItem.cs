using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BoardItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerMoveHandler, IPointerDownHandler, IPointerUpHandler
{
    Canvas canvas;

    public InventoryItem inventoryItem;
    RectTransform rectTransform;

    //The button on the board system that Instantiated this thing
    public GameObject btnObject;

    internal Slot assignedSlot = null;

    private bool bCanDrag = true;

    [SerializeField] LayerMask layerMask;

    //The item in the inventory this represents
    //public GameObject representedItem; 

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        if (canvas == null)
            print("Canvas Null");
        
        rectTransform = GetComponent<RectTransform>();

        TheBoardController.itemBeingDragged = this;

    }

    private void Update()
    {
        //if (bCanDrag)
        //{
        //    if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 100f, layerMask))
        //    {
        //        Debug.Log("RaycastHitting");
        //        transform.position = hitInfo.point;

        //    }
        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        TheBoardController.itemBeingDragged = null;

        //        //Check if it is in slot, if not delete the object else delete the button
        //        if (assignedSlot == null)
        //        {
        //            bCanDrag = false;
        //            Destroy(gameObject);
        //        }
        //        else
        //        {
        //            //Add the Board Item to List
        //            TheBoardController.Instance.boardItems.Add(this);
        //            bCanDrag = false;

        //            //PlaceItem
        //            assignedSlot.PlaceItem(this);

        //            //Destroy the button
        //            Destroy(btnObject);
        //        }
        //    }
        //}
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
    }

    

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Debug.Log("PointerMove");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("PointerUp");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //
    }
}
