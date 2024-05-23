using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using static UnityEngine.InputSystem.InputAction;

public class PauseMenu : Menu
{
    public MenuClassifier hudMenuClassifier;
    // Add this line to reference the level scene

    public void OnReturnToMainMenu()
    {
        MenuManager.Instance.GetMenu<MainMenu>(MenuManager.Instance.MainMenuClassifier)?.OnReturnToMainMenu();
        MenuManager.Instance.HideMenu(menuClassifier);
    }

    public void OnPauseGame()
    {
        Time.timeScale = 0.0f;
        MenuManager.Instance.HideMenu(menuClassifier);
    }

    public void OnContinueGame() {
    
        Time.timeScale = 1.0f;
        MenuManager.Instance.HideMenu(hudMenuClassifier);

    
    }

}
