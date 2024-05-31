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
    // Add this line to reference the level scen
    public InputAction pauseInputAction;

    private Camera Main;

    private void Awake()
    {
        Main = Camera.main;
    }

    private void OnEnable()
    {
        pauseInputAction.Enable();
        pauseInputAction.performed += OnPauseGamePerformed;
    }


    public void OnReturnToMainMenu()
    {
        MenuManager.Instance.GetMenu<MainMenu>(MenuManager.Instance.MainMenuClassifier)?.OnReturnToMainMenu();
        MenuManager.Instance.HideMenu(menuClassifier);

        Main.gameObject.SetActive(true);


    }

    private void OnPauseGamePerformed(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 1.0f)
        {
            OnPauseGame();
        }
        else
        {
            OnContinueGame();
        }
    }

    public void OnPauseGame()
    {
        Time.timeScale = 0.0f;
        MenuManager.Instance.ShowMenu(menuClassifier);
    }

    public void OnContinueGame()
    {
        Time.timeScale = 1.0f;
        MenuManager.Instance.HideMenu(hudMenuClassifier);
    }

}