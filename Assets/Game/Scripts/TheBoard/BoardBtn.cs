using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoardBtn : MonoBehaviour, IPointerDownHandler
{
    //The Inventory Item the button is representing (This is so we can Instantiate the Board actual thing on the board)
    [SerializeField] internal InventoryItem rep_Item;

    //Gameobjec / Image prefab on the board
    [SerializeField] GameObject boardItem;

    //This should be parented to what
    [SerializeField] Transform boardItemParent;

    public void InstantiateBoardItem()
    {
        GameObject g = Instantiate(boardItem, boardItemParent);
        g.transform.position = transform.position;
        BoardItem i = g.GetComponent<BoardItem>();
        i.inventoryItem = rep_Item;
        i.btnObject = gameObject;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InstantiateBoardItem();
    }
}
