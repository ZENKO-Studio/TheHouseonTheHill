using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleUISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] PuzzleUIController puzzleUIController;

    internal bool bValid;

    PuzzleUIItem slotItem;

    //This will have definations for required key items in the slot group
    PuzzleUIController puzzleController;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (PuzzleUIController.itemBeingDragged != null)
            PuzzleUIController.itemBeingDragged.assignedSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (PuzzleUIController.itemBeingDragged != null)
            PuzzleUIController.itemBeingDragged.assignedSlot = null;
    }

    internal void PlaceItem(PuzzleUIItem puzzleItem)
    {
        //Handle Existing Item
        if (slotItem != null)
        {
            //Destroy Slot Item
            Destroy(slotItem.gameObject);
            bValid = false;
        }

        slotItem = puzzleItem;
        slotItem.transform.parent = transform;
        slotItem.transform.localPosition = Vector2.zero;
        //TheBoardController.Instance.boardItems.Add(slotItem.inventoryItem);

        Validate();
    }

    internal void Validate()
    {
        if (puzzleUIController != null)
        {
            puzzleUIController.Validate();
        }
    }

}
