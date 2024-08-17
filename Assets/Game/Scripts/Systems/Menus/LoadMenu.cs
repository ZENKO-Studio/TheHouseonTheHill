
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour
{
    void OnEnable()
    {
        loadSlider.value = 0f;
    }

    [SerializeField] Slider loadSlider;
    void Update()
    {
        if (loadSlider)
            loadSlider.value = SceneLoader.Instance.LoadProgress;
    }
}
