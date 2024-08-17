using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EditModeRunner : MonoBehaviour
{
    [SerializeField] 
    GameObject m_gameObject;

    InventoryItem m_inventoryItem;


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
}
