using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleUIController : MonoBehaviour
{
    internal static PuzzleUIItem itemBeingDragged;

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
        throw new NotImplementedException();
    }
}
