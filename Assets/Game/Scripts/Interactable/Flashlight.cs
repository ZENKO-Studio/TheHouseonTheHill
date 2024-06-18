using UnityEngine;

[RequireComponent(typeof(Light))]
public class Flashlight : MonoBehaviour
{
    bool bIsOn = false;
    Light light;

    float charging = 100f;
    [SerializeField] float depletionRate = 5f;
    [SerializeField] float chargeRate = 2f;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }

        if (bIsOn)
        {
            charging -= depletionRate * Time.deltaTime;

            if(charging <= 0)
            { 
                bIsOn = false;
                light.enabled = false;
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
    }
}
