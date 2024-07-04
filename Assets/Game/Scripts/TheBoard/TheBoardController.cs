/**@Sami 
 * This handles all the stuff in the board
 * Has a List of all the board items and Slot Handlers 
 * It will check whether the item place in slots are valid and can enable / disable active slots
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static EventBus;

public class TheBoardController : Singleton<TheBoardController>
{
    InventoryHandler _inventory;

    [SerializeField] Transform _buttonArea; //Referenc to the Panel where buttons will be

    public GameObject boardListItemBtn; // Reference to the ItemButton prefab
    
    [SerializeField] GameObject boardUI;

    [SerializeField] Camera boardCam;

    List<SlotHandler> slotHandlers = new List<SlotHandler>();//This will be group of slots

    //When dragging and dropping, which item is actively being dragged
    internal static BoardItem itemBeingDragged;

    //This to be made scriptable as this will be used to save the board state
    public List<BoardItem> boardItems;

    private void OnToggleBoard(ToggleBoardEvent toggleEvent)
    {
        boardUI.SetActive(toggleEvent.IsOpen);
    }


    private void OnEnable()
    {
        EventBus.Subscribe<ToggleBoardEvent>(OnToggleBoard);
        boardUI.SetActive(false); // Ensure the inventory is initially hidden

        _inventory = InventoryHandler.Instance;
     
        PopulateBoardItemBtns();

        //Get all the slot handlers for the Board
        slotHandlers = GetComponentsInChildren<SlotHandler>().ToList<SlotHandler>();
        
        //EventBus.Subscribe<ItemAddedEvent>(OnItemAdded);
        //EventBus.Subscribe<ItemRemovedEvent>(OnItemRemoved);
    }

    private void OnDisable()
    {
        RemoveAllBtns();
        EventBus.Unsubscribe<ToggleBoardEvent>(OnToggleBoard);

        //EventBus.Unsubscribe<ItemAddedEvent>(OnItemAdded);
        //EventBus.Unsubscribe<ItemRemovedEvent>(OnItemRemoved);
    }

    private void RemoveAllBtns()
    {
        // Loop through all children of the transform
        for (int i = _buttonArea.childCount - 1; i >= 0; i--)
        {
            // Get the child at the current index
            Transform child = transform.GetChild(i);
            // Destroy the child game object
            Destroy(child.gameObject);
        }
    }

    //Called In Start
    void PopulateBoardItemBtns()
    {
        foreach(var v in _inventory.documents)
        {
            CreateBoardBtn(v.Key);
        }
        
        foreach(var v in _inventory.photos)
        {
            CreateBoardBtn(v.Key);
        }
    }

    internal void CreateBoardBtn(InventoryItem i)
    {
        GameObject g = Instantiate(boardListItemBtn, _buttonArea);
        g.GetComponent<Image>().sprite = i.itemIcon;

        BoardBtn b = g.GetComponent<BoardBtn>();
        b.rep_Item = i;
    }
}
