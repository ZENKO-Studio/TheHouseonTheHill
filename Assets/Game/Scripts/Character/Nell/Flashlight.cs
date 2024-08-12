using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Light))]
public class Flashlight : MonoBehaviour
{
    bool bIsOn = false;
    Light light;

    AudioSource audioSource;
 
    float charging = 100f;
    [SerializeField] float depletionRate = 5f;
    [SerializeField] float chargeRate = 2f;

    //Can be used to Updated the HUD for player and if we have health bars for NPCs
    [HideInInspector] public UnityEvent OnFlashLightToggle = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();
        light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.F))
        //{
        //    ToggleFlashlight();
        //}

        if (bIsOn)
        {
            charging -= depletionRate * Time.deltaTime;

            if(charging <= 0)
            { 
                bIsOn = false;
                light.enabled = false;
                OnFlashLightToggle?.Invoke();
            }

        }
        else
        {
            charging += chargeRate * Time.deltaTime;
        }
    }

    public void ToggleFlashlight()
    {
        if (!bIsOn && charging <= 0)
            return;

        bIsOn = !bIsOn;
        light.enabled = bIsOn;

        audioSource.Play();

        OnFlashLightToggle?.Invoke();
    }

    public bool IsOn()
    {
        return bIsOn; 
    }
}
