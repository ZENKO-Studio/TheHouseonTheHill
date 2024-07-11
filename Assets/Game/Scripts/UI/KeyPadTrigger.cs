using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadTrigger : MonoBehaviour
{
    [SerializeField] private GameObject keypadCanvas; // Reference to the Canvas GameObject

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the tag "Player"
        {
            keypadCanvas.SetActive(true); // Activate the canvas
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the tag "Player"
        {
            keypadCanvas.SetActive(false); // Deactivate the canvas
        }
    }
}
