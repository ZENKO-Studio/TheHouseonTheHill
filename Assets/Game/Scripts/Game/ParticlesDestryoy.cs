using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticlesDestryoy : MonoBehaviour
{
    public UnityEvent onInteract; 
    private void OnParticleCollision(GameObject other)
    {
        // Add any additional checks here, such as verifying the type of particle system
        Destroy(gameObject);
        onInteract?.Invoke();
       
    }
}
