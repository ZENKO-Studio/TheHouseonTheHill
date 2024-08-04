using UnityEngine;
using UnityEngine.Events;

public class SaltChargeHandler : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] int initialSaltCharges = 0;
    
    [Range(0, 10)]
    [SerializeField] int maxSaltCharges = 5;

    [Tooltip("How frequently can nell throw salt (Cooldown for Salt use)")]
    [SerializeField] int throwFreq = 3;

    int currentSaltCharges = 0;


    [SerializeField]
    Transform saltSpawnPosition;

    [SerializeField]
    GameObject saltParticles;

    [Range(0f, 10f)]
    [SerializeField] float saltRange = 5f;

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

        currentSaltCharges = initialSaltCharges;

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
       
        // Perform the OverlapSphere check
        Collider[] hitColliders = Physics.OverlapSphere(saltSpawnPosition.position, saltRange);

        // Iterate through the colliders and check for the tag
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Stalker stalker = hitCollider.GetComponent<Stalker>();
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
    public void AddSalt(int quantity = 1)
    {
        CurrentSaltCharges = (CurrentSaltCharges + quantity) > maxSaltCharges ? CurrentSaltCharges + quantity : maxSaltCharges;
        ResetSaltAbility();
    }
}
