using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyColliders : MonoBehaviour
{
    // Assign this in the Unity Inspector
    public Collider colliderToDestroy;

    private void OnTriggerEnter(Collider other)
    {

        if (colliderToDestroy != null)
        {
            Destroy(colliderToDestroy.gameObject);
        }
    }
}
