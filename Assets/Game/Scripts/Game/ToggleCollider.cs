using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCollider : MonoBehaviour
{
    public GameObject targetObject; // The GameObject with the collider you want to toggle

    private Collider targetCollider;

    void Start()
    {
        if (targetObject != null)
        {
            targetCollider = targetObject.GetComponent<Collider>();
            if (targetCollider == null)
            {
               // Debug.LogError("No Collider found on the target object.");
            }
        }
        else
        {
           // Debug.LogError("No target object assigned.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (targetCollider != null)
        {
            targetCollider.enabled = !targetCollider.enabled;
           // Debug.Log("Collider on target object is now " + (targetCollider.enabled ? "enabled" : "disabled"));
        }
    }
}
