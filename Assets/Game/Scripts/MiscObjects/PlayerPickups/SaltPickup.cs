using Game.Scripts.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltPickup : MonoBehaviour
{
    public GameObject interactPopup; //Popup to show when Player is Close

    [Tooltip("How much salt should this pickup add")]
    [SerializeField] int quanitity = 1;

    #region Golwing Part

    [SerializeField] bool bShouldGlow = false;

    public Material objectMaterial;

    public Color emissionColor = Color.yellow;
    public float maxGlowIntensity = 50.0f;
    public AnimationCurve intensityMultiplier;
    #endregion

    protected virtual void Start()
    {
        GetComponent<Collider>().isTrigger = true;

        if (bShouldGlow)
        {
            objectMaterial = GetComponentInChildren<Renderer>().material;
            Debug.Log($"{objectMaterial.name}");
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.playerRef.PlayerInteracted.AddListener(Interact);

            if (interactPopup != null)
            {
                interactPopup.SetActive(true);
            }

            if (bShouldGlow)
            {
                // Enable emission keyword
                objectMaterial.EnableKeyword("_EMISSION");

                // Set the emission color and intensity
                objectMaterial.SetColor("_EmissiveColor", emissionColor * intensityMultiplier.Evaluate(0));
            }
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && bShouldGlow)
        {
            float emission = maxGlowIntensity * intensityMultiplier.Evaluate(Time.time % 1);

            // Set the emission color and intensity
            objectMaterial.SetColor("_EmissiveColor", emissionColor * emission);

        }

    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.playerRef.PlayerInteracted.AddListener(Interact);

            if (interactPopup != null)
            {
                interactPopup.SetActive(false);
            }
            if (bShouldGlow)
            {
                // Set the emission color and intensity
                objectMaterial.SetColor("_EmissiveColor", emissionColor * intensityMultiplier.Evaluate(0));

                // Disable emission keyword
                objectMaterial.DisableKeyword("_EMISSION");

            }
        }
    }

    public virtual void Interact()
    {
        GameManager.Instance.playerRef.saltChargeHandler.AddSalt(quanitity);

        Destroy(gameObject);
    }

}
