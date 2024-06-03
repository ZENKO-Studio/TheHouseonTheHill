using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class HUDMenu : Menu
{
    public MenuClassifier pauseMenuClassifier;
    public InputAction pauseAction;
    public Camera gameCamera; // This will be dynamically assigned

    private Menu pauseMenu;

    private void OnEnable()
    {
        if (pauseAction == null)
        {
            Debug.LogError("pauseAction is not assigned.");
            return;
        }

        pauseAction.Enable();
        Debug.Log("Pause Action Enabled");
        pauseAction.performed += OnPausePerformed;

        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
    }

    private void OnDisable()
    {
        if (pauseAction != null)
        {
            pauseAction.performed -= OnPausePerformed;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from the sceneLoaded event
    }

    protected override void Start()
    {
        base.Start();
        pauseMenu = MenuManager.Instance.GetMenu<Menu>(pauseMenuClassifier);
        if (pauseMenu == null)
        {
            Debug.LogError("Failed to find pause menu with classifier: " + pauseMenuClassifier);
        }

        AssignCamera(); // Assign the camera if it exists in the current scene
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        OnPauseGame();
    }

    public void OnPauseGame()
    {
        Time.timeScale = 0.0f;
        MenuManager.Instance.ShowMenu(pauseMenuClassifier);
        Debug.Log("Game Paused and Pause Menu Shown");

        if (gameCamera != null)
        {
            gameCamera.enabled = false;
            Debug.Log("Game Camera Disabled");
        }
        else
        {
            Debug.LogError("Game Camera is not assigned.");
        }
    }

    public void OnResumeGame()
    {
        Time.timeScale = 1.0f;
        MenuManager.Instance.HideMenu(pauseMenuClassifier);
        Debug.Log("Game Resumed and Pause Menu Hidden");

        if (gameCamera != null)
        {
            gameCamera.enabled = true;
            Debug.Log("Game Camera Enabled");
        }
        else
        {
            Debug.LogError("Game Camera is not assigned.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignCamera(); // Assign the camera when a new scene is loaded
    }

    private void AssignCamera()
    {
        gameCamera = Camera.main; // Assign the main camera

        if (gameCamera == null)
        {
            Debug.LogError("No camera found in the scene.");
        }
        else
        {
            Debug.Log("Camera assigned: " + gameCamera.name);
        }
    }
}
