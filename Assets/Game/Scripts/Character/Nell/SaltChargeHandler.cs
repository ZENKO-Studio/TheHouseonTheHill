using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltChargeHandler : MonoBehaviour
{
    [SerializeField]
    Transform saltSpawnPosition;

    [SerializeField]
    ParticleSystem saltParticles;

    [Range(0f, 10f)]
    [SerializeField] float throwDistance = 5f;

    bool bCanThrowSalt = false;

    public void ThrowSaltCharge()
    {
        if (bCanThrowSalt)
        {
            if(saltParticles != null)
                Instantiate(saltParticles, saltSpawnPosition.position, saltSpawnPosition.rotation); //or
            //saltParticles.Emit(100);

            if (Physics.SphereCast(new Ray(saltSpawnPosition.position, saltSpawnPosition.forward), .5f, out RaycastHit hitInfo, throwDistance))
            {
                // Code to execute if the sphere cast hits something
                if(hitInfo.collider.CompareTag("Enemy"))
                {
                    Stalker stalker = hitInfo.collider.GetComponent<Stalker>();
                    if (stalker)
                        stalker.GetStunned();
                }
            }

        }

        
    }
}
