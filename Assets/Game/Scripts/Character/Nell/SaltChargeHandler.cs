using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltChargeHandler : MonoBehaviour
{
    [Range(3, 10)]
    [SerializeField] int maxSaltCharges = 5;

    [Tooltip("How frequently can nell throw salt (Cooldown for Salt use)")]
    [SerializeField] int throwFreq = 3;

    int currentSaltCharges = 5;

    [SerializeField]
    Transform saltSpawnPosition;

    [SerializeField]
    GameObject saltParticles;

    [Range(0f, 10f)]
    [SerializeField] float throwDistance = 5f;

    bool bCanThrowSalt = false;

    NellController nellController;

    private void Awake()
    {
        nellController = GetComponent<NellController>();

        ResetSaltAbility();
    }

    public void ThrowSalt()
    {
        if (bCanThrowSalt)
        {
            nellController.nellsAnimator.SetTrigger("ThrowSalt");
            bCanThrowSalt = false;

            currentSaltCharges--;

            Invoke(nameof(ResetSaltAbility), throwFreq);
        }
    }

    public void SaltThrown(AnimationEvent animationEvent)
    {
        if (saltParticles != null)
            Instantiate(saltParticles, saltSpawnPosition.position, saltSpawnPosition.rotation); 
        
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

    private void ResetSaltAbility()
    {
        if (currentSaltCharges > 0)
        {
            bCanThrowSalt = true;
        }
    }

    //When the salt is picked up
    public void AddSalt()
    {
        currentSaltCharges = currentSaltCharges == maxSaltCharges ? currentSaltCharges + 1 : maxSaltCharges;
    }
}
