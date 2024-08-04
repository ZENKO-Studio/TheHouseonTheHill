using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleUISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] PuzzleUIController puzzleUIController;

    [SerializeField] int reqPieceNo = -1;

    internal bool bValid;

    internal PuzzleUIItem slotItem;

    //This will have definations for required key items in the slot group
    PuzzleUIController puzzleController;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (PuzzleUIController.itemBeingDragged != null && slotItem == null)
            PuzzleUIController.itemBeingDragged.assignedSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (PuzzleUIController.itemBeingDragged != null)
            PuzzleUIController.itemBeingDragged.assignedSlot = null;

        if(slotItem == PuzzleUIController.itemBeingDragged)
            slotItem = null;
    }

    internal void PlaceItem(PuzzleUIItem puzzleItem)
    {
        
        slotItem = puzzleItem;
        slotItem.transform.parent = transform;
        slotItem.transform.localPosition = Vector2.zero;
        //TheBoardController.Instance.boardItems.Add(slotItem.inventoryItem);

        Validate();

        if(!bValid)
            slotItem.GetComponent<CanvasGroup>().blocksRaycasts = true;
        
    }

    internal void Validate()
    {
        if(reqPieceNo == slotItem.no)
        {
            bValid = true;
        }

        if (puzzleUIController != null)
        {
            //puzzleUIController.Validate();
        }
    }

    public void RemoveSlotItem()
    {
        slotItem.ResetPuzzlePiece();
        slotItem = null;
    }

}
