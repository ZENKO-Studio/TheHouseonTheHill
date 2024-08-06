using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        MenuManager.Instance.AddMenuObject(gameObject, MenuType.MainMenu);
    }

    public void OnStartButton()
    {
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
    }

    public void OnApplicationQuit()
    {

        Application.Quit();
        

    }

    

}
