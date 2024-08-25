using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    private void Start()
    {
        MenuManager.Instance.AddMenuObject(gameObject, MenuType.GameOveMenu);
        gameObject.SetActive(false); // Ensure the Pause is initially hidden
    }

    public void OnRetryBtn()
    {
        GameManager.Instance.RespawnPlayer();
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
