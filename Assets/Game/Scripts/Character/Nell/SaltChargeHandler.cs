using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaltChargeHandler : MonoBehaviour
{
    [Range(3, 10)]
    [SerializeField] int maxSaltCharges = 5;

    [Tooltip("How frequently can nell throw salt (Cooldown for Salt use)")]
    [SerializeField] int throwFreq = 3;

    int currentSaltCharges = 4;


    [SerializeField]
    Transform saltSpawnPosition;

    [SerializeField]
    GameObject saltParticles;

    [Range(0f, 10f)]
    [SerializeField] float throwDistance = 5f;

    bool bCanThrowSalt = false;

    NellController nellController;

    //Can be used to Updated the HUD for player and if we have health bars for NPCs
    public UnityEvent OnSaltChanged = new UnityEvent();

    public int CurrentSaltCharges { get => currentSaltCharges; set { currentSaltCharges = value;
            OnSaltChanged?.Invoke();
        } }

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

            CurrentSaltCharges--;

            Invoke(nameof(ResetSaltAbility), throwFreq);
        }
    }

    GameObject saltParts = null;

    public void SaltThrown(AnimationEvent animationEvent)
    {
        if (saltParticles != null)
            saltParts = Instantiate(saltParticles, saltSpawnPosition.position, saltSpawnPosition.rotation); 
        
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
        if (saltParts != null)
        {
            Destroy(saltParts);
        }

        if (CurrentSaltCharges > 0)
        {
            bCanThrowSalt = true;
        }
    }

    //When the salt is picked up
    public void AddSalt()
    {
        CurrentSaltCharges = CurrentSaltCharges == maxSaltCharges ? CurrentSaltCharges + 1 : maxSaltCharges;
    }
}
