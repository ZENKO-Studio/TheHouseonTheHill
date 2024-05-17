using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerEffect : MonoBehaviour
{
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.1f;

    private Light pointLight;
    private float targetIntensity;

    void Start()
    {
        pointLight = GetComponent<Light>();
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        InvokeRepeating("Flicker", 0f, flickerSpeed);
    }

    void Flicker()
    {
        targetIntensity = Random.Range(minIntensity, maxIntensity);
    }

    void Update()
    {
        pointLight.intensity = Mathf.Lerp(pointLight.intensity, targetIntensity, Time.deltaTime * 5f);
    }
}
