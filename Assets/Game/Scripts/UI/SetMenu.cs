using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class SetMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Slider contrastSlider;
    public Slider exposureSlider;
    public Slider brightnessSlider;
    public Slider volumeSlider;
    public Volume volume;

    private Resolution[] resolutions;

    void Start()
    {
        MenuManager.Instance.AddMenuObject(gameObject, MenuType.OptionsMenu);

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        contrastSlider.onValueChanged.AddListener(SetContrast);
        exposureSlider.onValueChanged.AddListener(SetExposure);
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetContrast(float contrast)
    {
        if (volume != null)
        {
            ColorAdjustments colorAdjustments;
            if (volume.profile.TryGet(out colorAdjustments))
            {
                colorAdjustments.contrast.value = contrast;
            }
        }
    }

    public void SetExposure(float exposure)
    {
        if (volume != null)
        {
            Exposure exposureComponent;
            if (volume.profile.TryGet(out exposureComponent))
            {
                exposureComponent.fixedExposure.value = exposure;
            }
        }
    }

    public void SetBrightness(float brightness)
    {
        if (volume != null)
        {
            ColorAdjustments colorAdjustments;
            if (volume.profile.TryGet(out colorAdjustments))
            {
                colorAdjustments.postExposure.value = brightness;
            }
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    
    

    public void QuitGame()
    {
        Application.Quit();
    }
}
