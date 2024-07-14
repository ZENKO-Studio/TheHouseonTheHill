using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SlotHandler : MonoBehaviour
{
    //If all the child Slots are valid
    bool bValid = false;

    [Tooltip("This is where all the necessary document ids have to be added")]
    public List<int> itemIds = new List<int>();

    [Tooltip("This is where answers to this question will be present (Like actual solts in UI")] 
    [SerializeField] List<Slot> slots = new List<Slot>();

    [SerializeField] Color validColor;
    [SerializeField] Color invalidColor;

    private void Start()
    {
        slots = GetComponentsInChildren<Slot>().ToList<Slot>();
        foreach (Slot slot in slots)
        {
            slot.SetSlotHandler(this);
        }
    }

    public void Validate()
    {
        foreach (Slot slot in slots)
        {
            if (!slot.bValid)
            {
                bValid = false;
                GetComponent<Image>().color = invalidColor;
                return;
            }
        }

        bValid = true;
        GetComponent<Image>().color = validColor;


    }

    public bool IsValid()
    {
        return bValid;
    }
}
