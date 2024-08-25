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

    public void OnStartButton(int i = 1)
    {
        GameManager.Instance.StartLevel(i);
    }



    public void OnApplicationQuit()
    {

        Application.Quit();
        

    }

    

}
