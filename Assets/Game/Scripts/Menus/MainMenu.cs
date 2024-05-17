using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu
{
    public SceneReference Level;
    public MenuClassifier hudClassifier;
    public MenuClassifier optionsMenuClassifier; // Add a reference to the options menu classifier

    // Method to load the level scene
    public void OnLoadLevel()
    {
        SceneLoader.Instance.LoadScene(Level);
        MenuManager.Instance.HideMenu(menuClassifier);

        MenuManager.Instance.ShowMenu(hudClassifier);
    }

    // Method to return to the main menu
    public void OnReturnToMainMenu()
    {
        Time.timeScale = 1.0f;

        MenuManager.Instance.ShowMenu(MenuManager.Instance.LoadingScreenClassifier);
        MenuManager.Instance.HideMenu(MenuManager.Instance.HUDMenuClassifier);

        SceneLoader.Instance.OnScenesUnLoadedEvent += AllScenesUnloaded;
        SceneLoader.Instance.UnLoadAllLoadedScenes();
    }

    // Method called when all scenes are unloaded
    private void AllScenesUnloaded()
    {
        SceneLoader.Instance.OnScenesUnLoadedEvent -= AllScenesUnloaded;

        MenuManager.Instance.ShowMenu(MenuManager.Instance.MainMenuClassifier);
        MenuManager.Instance.HideMenu(MenuManager.Instance.LoadingScreenClassifier);
    }

    // Method to open the options menu
    public void OnOpenOptionsMenu()
    {
        MenuManager.Instance.ShowMenu(optionsMenuClassifier);
        MenuManager.Instance.HideMenu(menuClassifier);
    }
}
