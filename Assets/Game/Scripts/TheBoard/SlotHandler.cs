using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHandler : MonoBehaviour
{
    //If all the child Slots are valid
    bool bValid = false;

    [Tooltip("This is where all the necessary document ids have to be added")]
    public List<int> itemIds = new List<int>();

    [Tooltip("This is where answers to this question will be present (Like actual solts in UI")] 
    [SerializeField] List<Slot> slots = new List<Slot>();

    void Validate()
    {
        foreach (Slot slot in slots)
        {
            if (!slot.bValid)
            {
                bValid = false;
                return;
            }
        }
        bValid = true;
    }

    public bool IsValid()
    {
        return bValid;
    }
}
