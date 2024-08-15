using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using static UnityEngine.InputSystem.InputAction;

public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        MenuManager.Instance.AddMenuObject(gameObject, MenuType.PauseMenu);
        gameObject.SetActive(false); // Ensure the Pause is initially hidden
    }

    public void OnResumeBtn()
    {
        GameManager.Instance.ResumeGame();
    }

    public void OnMainMenuBtn()
    {
        GameManager.Instance.EndGame();
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
