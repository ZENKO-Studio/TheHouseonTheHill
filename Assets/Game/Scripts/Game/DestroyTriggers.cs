using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTriggers : MonoBehaviour
{
    [SerializeField] private GameObject Trigger;
    private void OnTriggerEnter(Collider other)
    {
       
        // You can add a condition to check for specific objects if needed
        if (other.CompareTag("Player")) 
        {
            // Do something when the trigger is activated
            //Debug.Log("Trigger activated!");
            if (Trigger != null)
            {
                Destroy(Trigger);
            
            } 
            // Destroy the game object after triggering
            Destroy(gameObject);
        }
    }
}
