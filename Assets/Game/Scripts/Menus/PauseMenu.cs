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
    public InputAction pauseInputAction;

    private Camera Main;

    private void Awake()
    {
        Main = Camera.main;
        if (Main == null)
        {
            Debug.LogError("Main Camera not found.");
        }
        pauseInputAction = new InputAction(binding: "<Keyboard>/escape");
    }

    private void OnEnable()
    {
        if (pauseInputAction == null)
        {
            Debug.LogError("pauseInputAction is not assigned.");
            return;
        }

        pauseInputAction.Enable();
        pauseInputAction.performed += OnPauseGamePerformed;
        Debug.Log("pauseInputAction enabled and callback assigned.");
    }

    private void OnDisable()
    {
        if (pauseInputAction != null)
        {
            pauseInputAction.performed -= OnPauseGamePerformed;
            
        }
    }

    public void OnReturnToMainMenu()
    {
        MenuManager.Instance.GetMenu<MainMenu>(MenuManager.Instance.MainMenuClassifier)?.OnReturnToMainMenu();
        MenuManager.Instance.HideMenu(menuClassifier);

        if (Main != null)
        {
            Main.gameObject.SetActive(true);
        }
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
