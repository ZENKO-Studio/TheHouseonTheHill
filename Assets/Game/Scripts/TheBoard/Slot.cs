using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    internal bool bValid;

    BoardItem slotItem;

    //This will have definations for required key items in the slot group
    SlotHandler slotHandler;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("PointerEntered");
        if (TheBoardController.itemBeingDragged != null)
            TheBoardController.itemBeingDragged.assignedSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("PointerLeft");
        if (TheBoardController.itemBeingDragged != null)
            TheBoardController.itemBeingDragged.assignedSlot = null;
    }

    public void SetSlotHandler(SlotHandler handler)
    {
        slotHandler = handler;
    }

    internal void PlaceItem(BoardItem boardItem)
    {
        //Handle Existing Item
        if(slotItem != null)
        {
            //Destroy Slot Item
            TheBoardController.Instance.boardItems.Remove(slotItem);
            TheBoardController.Instance.SetBoardItems(slotItem.inventoryItem);
            Destroy(slotItem.gameObject);
        }

        slotItem = boardItem;
        slotItem.transform.parent = transform;
        slotItem.transform.localPosition = Vector2.zero;

        Validate();
    }

    internal void Validate()
    {
        if (slotHandler != null)
        {
            foreach (int docId in slotHandler.itemIds)
            {
                if(slotItem.inventoryItem.itemId == docId)
                    bValid = true;
            }
        }
    }

}

