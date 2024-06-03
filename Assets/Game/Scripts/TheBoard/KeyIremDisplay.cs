using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyIremDisplay : MonoBehaviour
{

    public Transform keyItemDisplayArea; // Parent object to hold displayed key items


    public void DisplayKeyItem(GameObject keyItemPrefab)
    {
        GameObject displayObject = Instantiate(keyItemPrefab, keyItemDisplayArea);
        displayObject.layer = LayerMask.NameToLayer("UI");
    }
}
