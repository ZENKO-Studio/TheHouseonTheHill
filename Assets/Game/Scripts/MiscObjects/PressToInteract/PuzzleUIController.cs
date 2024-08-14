using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleUIController : MonoBehaviour
{
    internal static PuzzleUIItem itemBeingDragged;
    internal JigsawPuzzle puzzleRef;

    internal UnityEvent OnPuzzleInit = new UnityEvent();

    internal bool bValid = false;

    public void ResetPuzzle()
    {
        foreach (PuzzleUISlot slot in GetComponentsInChildren<PuzzleUISlot>())
        {
            if (slot.slotItem != null && !slot.bValid)
                slot.RemoveSlotItem();
        }
    }

    internal void Validate()
    {
        foreach(PuzzleUISlot slot in GetComponentsInChildren(typeof(PuzzleUISlot)))
        {
            if (!slot.bValid)
                return;
        }

        bValid = true;
    }
}
