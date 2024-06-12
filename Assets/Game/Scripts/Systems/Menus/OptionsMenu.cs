using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : Menu
{
    public MenuClassifier previousMenuClassifier;

    private Slider volumeSlider;
    private TMP_Dropdown resolutionDropdown;
    private Button applyButton;
    private Button backButton;
    private Button quitButton;

    private Resolution[] resolutions;
    private List<string> resolutionOptions = new List<string>();

    protected override void Start()
    {
        startingMenuState = StartingMenuState.Disable; // Ensure the options menu is disabled at start
        base.Start();

        // Create a Canvas for the layout
        GameObject canvasGO = new GameObject("Canvas");
        canvasGO.transform.SetParent(this.transform);
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Create a VerticalLayoutGroup for the layout
        GameObject layoutGroupGO = new GameObject("LayoutGroup");
        layoutGroupGO.transform.SetParent(canvasGO.transform);
        VerticalLayoutGroup layoutGroup = layoutGroupGO.AddComponent<VerticalLayoutGroup>();
        layoutGroup.childAlignment = TextAnchor.MiddleCenter;
        layoutGroup.spacing = 10;
        layoutGroup.childControlHeight = true;
        layoutGroup.childControlWidth = true;

        // Add ContentSizeFitter to adjust the size of the layout group
        ContentSizeFitter sizeFitter = layoutGroupGO.AddComponent<ContentSizeFitter>();
        sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        // Create and set up the volume slider
        CreateSlider(layoutGroupGO, "VolumeSlider", "Volume", SetVolume);

        // Create and set up the resolution dropdown
        CreateDropdown(layoutGroupGO, "ResolutionDropdown", "Resolution", SetResolution);

        // Create and set up the apply button
        CreateButton(layoutGroupGO, "ApplyButton", "Apply", ApplySettings);

        // Create and set up the back button
        CreateButton(layoutGroupGO, "BackButton", "Back", OnReturnToPreviousMenu);

        // Create and set up the quit button
        CreateButton(layoutGroupGO, "QuitButton", "Quit", QuitGame);

        // Adjust RectTransform of the layout group for better positioning
        RectTransform layoutGroupRect = layoutGroupGO.GetComponent<RectTransform>();
        layoutGroupRect.anchorMin = new Vector2(0.5f, 0.5f);
        layoutGroupRect.anchorMax = new Vector2(0.5f, 0.5f);
        layoutGroupRect.pivot = new Vector2(0.5f, 0.5f);
        layoutGroupRect.anchoredPosition = Vector2.zero;
    }

    private void CreateSlider(GameObject parent, string name, string labelText, UnityEngine.Events.UnityAction<float> onValueChanged)
    {
        GameObject sliderGO = new GameObject(name);
        sliderGO.transform.SetParent(parent.transform);
        sliderGO.AddComponent<RectTransform>();
        Slider slider = sliderGO.AddComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = AudioListener.volume;
        slider.onValueChanged.AddListener(onValueChanged);

        AddLabel(sliderGO, labelText);
    }

    private void CreateDropdown(GameObject parent, string name, string labelText, UnityEngine.Events.UnityAction<int> onValueChanged)
    {
        GameObject dropdownGO = new GameObject(name);
        dropdownGO.transform.SetParent(parent.transform);
        dropdownGO.AddComponent<RectTransform>();
        TMP_Dropdown dropdown = dropdownGO.AddComponent<TMP_Dropdown>();

        resolutions = Screen.resolutions;
        resolutionOptions.Clear();

        foreach (Resolution resolution in resolutions)
        {
            resolutionOptions.Add(resolution.width + " x " + resolution.height);
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(resolutionOptions);
        dropdown.value = GetCurrentResolutionIndex();
        dropdown.RefreshShownValue();
        dropdown.onValueChanged.AddListener(onValueChanged);

        AddLabel(dropdownGO, labelText);
    }

    private void CreateButton(GameObject parent, string name, string buttonText, UnityEngine.Events.UnityAction onClickAction)
    {
        GameObject buttonGO = new GameObject(name);
        buttonGO.transform.SetParent(parent.transform);
        buttonGO.AddComponent<RectTransform>();
        Button button = buttonGO.AddComponent<Button>();
        button.onClick.AddListener(onClickAction);

        GameObject textGO = new GameObject("Text");
        textGO.transform.SetParent(buttonGO.transform);
        TextMeshProUGUI textMesh = textGO.AddComponent<TextMeshProUGUI>();
        textMesh.text = buttonText;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.rectTransform.sizeDelta = new Vector2(160, 30); // Set size for the button text
    }

    private void AddLabel(GameObject parent, string labelText)
    {
        GameObject labelGO = new GameObject("Label");
        labelGO.transform.SetParent(parent.transform);
        TextMeshProUGUI label = labelGO.AddComponent<TextMeshProUGUI>();
        label.text = labelText;
        label.alignment = TextAlignmentOptions.Center;
        RectTransform rectTransform = labelGO.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 1f);
        rectTransform.anchorMax = new Vector2(0.5f, 1f);
        rectTransform.pivot = new Vector2(0.5f, 1f);
        rectTransform.anchoredPosition = new Vector2(0, 20); // Adjust position relative to parent
    }

    private void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    private int GetCurrentResolutionIndex()
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                return i;
            }
        }
        return 0;
    }

    private void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void ApplySettings()
    {
        // Placeholder for applying settings logic
        Debug.Log("Settings applied.");
    }

    public void OnReturnToPreviousMenu()
    {
        MenuManager.Instance.ShowMenu(previousMenuClassifier);
        MenuManager.Instance.HideMenu(menuClassifier);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
