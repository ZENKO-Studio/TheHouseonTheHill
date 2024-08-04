using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject interactPopup; //Popup to show when Player is Close

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

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && bShouldGlow)
        {
            float emission = maxGlowIntensity * intensityMultiplier.Evaluate(Time.time % 1);

            // Set the emission color and intensity
            objectMaterial.SetColor("_EmissiveColor", emissionColor * emission);

        }

    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.playerRef.PlayerInteracted.RemoveListener(Interact);

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
    }

}
