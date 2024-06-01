using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTriggers : MonoBehaviour
{
    public GameObject objectToDisable;

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player has a tag "Player"
        {
            objectToDisable.SetActive(false);
        }
    }
}
