using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDestryoy : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        // Add any additional checks here, such as verifying the type of particle system
        Destroy(gameObject);
    }
}
