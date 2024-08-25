using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EditModeRunner : MonoBehaviour
{
    [SerializeField] 
    GameObject m_gameObject;

    InventoryItem m_inventoryItem;

    [SerializeField] Transform pieceHolder;

    [SerializeField] Transform pieceFrame;

    List<PuzzleUIItem> puzzleUIItems = new List<PuzzleUIItem>();


    [SerializeField] 
    List<Sprite> sprites = new List<Sprite>();
    
    [SerializeField] 
    List<Material> materials = new List<Material>();



    //[ExecuteInEditMode]
    // Start is called before the first frame update
    void Start()
    {
       //GeneratePuzzlePieces();
    }

    void GeneratePuzzlePieces()
    {
        for (int i = 0; i < sprites.Count; i++) 
        {
            InventoryItem inventoryItem = Instantiate(m_gameObject).GetComponent<InventoryItem>();
            inventoryItem.itemName = "PuzzlePiece";
            inventoryItem.itemIcon = sprites[i];
            inventoryItem.itemPreview = inventoryItem.gameObject;
            inventoryItem.itemDescription = "One of the piece of puzzle";
            inventoryItem.itemType = ItemType.UsableObj;
            inventoryItem.itemId = i + 1;
            inventoryItem.gameObject.name = $"PuzzlePiece{i + 1}";

            print($"{inventoryItem.transform.GetChild(0).name}");
            //print($"{inventoryItem.transform.GetChild(2).name} {inventoryItem.transform.GetChild(1).name} {inventoryItem.transform.GetChild(0).name}");

            inventoryItem.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material = materials[i];
        }
    }
    
   

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSubButtons()
    {
       
        foreach (var image in transform.GetComponentsInChildren<Transform>())
        {
            if (image != transform)
                { 

            GameObject pieceParent = Instantiate(new GameObject(), transform);
            image.SetParent(pieceParent.transform);
            RectTransform t = image.GetComponent<RectTransform>();
            t.anchorMin = new Vector2(0.5f, 0.5f);
            t.anchorMax = new Vector2(0.5f, 0.5f);
            t.pivot = new Vector2(0.5f, 0.5f);
            t.anchoredPosition = new Vector2(0, 0);
            pieceParent.name = $"Piece {image.GetComponent<PuzzleUIItem>().no}";
            }
        }    
    }

    public void SolvePuzzle()
    {
        puzzleUIItems = pieceHolder.GetComponentsInChildren<PuzzleUIItem>().ToList();

        Debug.Log($"{pieceFrame.childCount}");

        foreach (var item in puzzleUIItems)
        {
            item.transform.parent.SetParent(pieceFrame.GetChild(item.no - 1));
            RectTransform t = item.transform.parent.GetComponent<RectTransform>();
            t.anchorMin = new Vector2(0.5f, 0.5f);
            t.anchorMax = new Vector2(0.5f, 0.5f);
            t.pivot = new Vector2(0.5f, 0.5f);
            t.anchoredPosition = new Vector2(0, 0);
        }
    }
}
