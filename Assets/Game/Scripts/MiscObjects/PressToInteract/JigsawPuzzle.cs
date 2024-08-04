using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawPuzzle : InteractableObject
{
    //Whether player has all pieces
    bool bAllPiecesAvailable = false;

    [SerializeField] GameObject puzzleUI = null;

    //Override OnTriggerEnter if wanna play First Dialogue

    public override void Interact()
    {
        //Check the state and display dialogues 

        if (puzzleUI != null)
        {
            puzzleUI.SetActive(true);
        }

        if(bAllPiecesAvailable)
        {
            //UI Controller . StartPuzzle();
        }
        else
        {
            //Play Dialogue "Seems like some pieces are missing, I need to find them"
        }
    }
}
