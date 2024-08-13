using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JigsawPuzzle : InteractableObject
{
    //The pieces that are already near the puzzle
    [SerializeField] List<GameObject> initialPuzzlePieces = new List<GameObject>();

    internal List<PuzzleUIItem> placedItems = new List<PuzzleUIItem>();

    enum PuzzleState
    {
        Start,
        Identified,
        Unusable,
        Usable,
        Used
    }

    PuzzleState puzzleState = PuzzleState.Start;

    //Whether player has all pieces
    bool bAllPiecesAvailable = false;

    [SerializeField] GameObject puzzleUI = null;

    [Header("Dialogue Setup")]

    [Tooltip("Dialogue that will be played once when player enters the trigger for the first time")]
    [SerializeField] List<string> initialLines = new List<string>();

    [Tooltip("Dialogue that will be played once when player enters the trigger and do not have required items")]
    [SerializeField] List<string> linesWhenUnusable = new List<string>();

    [Tooltip("Dialogue that will be played once when player enters the trigger have all the items")]
    [SerializeField] List<string> linesWhenUsable = new List<string>();

    //Override OnTriggerEnter if wanna play First Dialogue
    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (puzzleState == PuzzleState.Start)
        {
            GameManager.Instance.playerHud.UpdateDialogueText(initialLines[Random.Range(0, initialLines.Count)], 3);
            puzzleState = PuzzleState.Identified;
            return;
        }

        EnableInteraction();
    }

    float t = 0f;

    protected override void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if(puzzleState == PuzzleState.Identified)
        {
            t += Time.deltaTime;
            if (t > 2f)
            {
                EnableInteraction();
                return;
            }
        }

        base.OnTriggerStay(other);
    }

    void EnableInteraction()
    {
        GameManager.Instance.playerRef.PlayerInteracted.AddListener(Interact);

        if (interactPopup != null)
        {
            interactPopup.SetActive(true);
        }

        //if (bShouldGlow)
        //{
        //    // Enable emission keyword
        //    objectMaterial.EnableKeyword("_EMISSION");

        //    // Set the emission color and intensity
        //    objectMaterial.SetColor("_EmissiveColor", emissionColor * intensityMultiplier.Evaluate(0));
        //}
    }

    public override void Interact()
    {
        //First Interact; give player the initial pieces 
        if(puzzleState == PuzzleState.Identified)
        {
            GiveInitialPieces();
        }

        bAllPiecesAvailable = CheckIfUsable();
        //Check the state and display dialogues 

        if (puzzleUI != null)
        {
            puzzleUI.SetActive(true);
            puzzleUI.GetComponent<PuzzleUIController>().puzzleRef = this;
        }

        if(bAllPiecesAvailable)
        {
            GameManager.Instance.playerHud.UpdateDialogueText(linesWhenUsable[Random.Range(0, linesWhenUsable.Count)], 3);
        }
        else
        {
            GameManager.Instance.playerHud.UpdateDialogueText(linesWhenUnusable[Random.Range(0, linesWhenUnusable.Count)], 3);
        }
    }

    private bool CheckIfUsable()
    {
        for(int i = 1; i <= 9; i++)
        {
            if (!InventoryHandler.Instance.HasUsableItem(i) && placedItems.Find(item => item.no == i) == null)
                return false;
        }

        return true;
    }

    //Give all the initial pieces to the player
    private void GiveInitialPieces()
    {
        for (int i = 0; i < initialPuzzlePieces.Count; i++)
        {
            InventoryItem iItem = Instantiate(initialPuzzlePieces[0]).GetComponent<InventoryItem>();
            InventoryHandler.Instance.AddItem(iItem);
            iItem.bInteractable = false;
            iItem.gameObject.SetActive(false);
            initialPuzzlePieces.RemoveAt(0);
        }
    }
}
