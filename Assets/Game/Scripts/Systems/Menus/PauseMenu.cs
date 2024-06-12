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
    // Add this line to reference the level scen
    private Players pauseInputAction;

    private Camera Main;

    protected override void Awake()
    {
        base.Awake();
        Main = Camera.main;
        pauseInputAction = new Players();
    }

    private void OnEnable()
    {
        pauseInputAction.PlayerMap.Enable();
        pauseInputAction.PlayerMap.Pause.Enable();
        pauseInputAction.PlayerMap.Pause.performed += OnPauseGamePerformed;
    }


    public void OnReturnToMainMenu()
    {
        MenuManager.Instance.GetMenu<MainMenu>(MenuManager.Instance.MainMenuClassifier)?.OnReturnToMainMenu();
        MenuManager.Instance.HideMenu(menuClassifier);

        Main.gameObject.SetActive(true);
    }

    private void OnPauseGamePerformed(InputAction.CallbackContext context)
    {
        Debug.Log("i am sami's best friend");
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
        MenuManager.Instance.HideMenu(menuClassifier);
    }
}
