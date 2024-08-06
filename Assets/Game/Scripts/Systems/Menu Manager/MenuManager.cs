using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public enum MenuType
{
    MainMenu,
    PauseMenu,
    OptionsMenu,
    HUDMenu,
    SceneLoadMenu
}

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject hudMenu;
    [SerializeField] GameObject sceneLoadMenu;

    internal void AddMenuObject(GameObject menuObject, MenuType menuType)
    {
        switch (menuType)
        {
            case MenuType.MainMenu:
                mainMenu = menuObject;
                break;
            case MenuType.PauseMenu:
                pauseMenu = menuObject;
                break;
            case MenuType.OptionsMenu:
                optionsMenu = menuObject;
                break;
            case MenuType.HUDMenu:
                hudMenu = menuObject;
                break;
            case MenuType.SceneLoadMenu:
                sceneLoadMenu = menuObject;
                break;
        }
    }

    internal void RemoveMenuObject(MenuType menuType)
    {
        switch (menuType)
        {
            case MenuType.MainMenu:
                mainMenu = null;
                break;
            case MenuType.PauseMenu:
                pauseMenu = null;
                break;
            case MenuType.OptionsMenu:
                optionsMenu = null;
                break;
            case MenuType.HUDMenu:
                hudMenu = null;
                break;
            case MenuType.SceneLoadMenu:
                sceneLoadMenu = null;
                break;
        }
    }

    internal void HideMenu(MenuType menuType)
    {
        switch (menuType)
        {
            case MenuType.MainMenu:
                mainMenu.SetActive(false);
                break;
            case MenuType.PauseMenu:
                pauseMenu.SetActive(false);
                break;
            case MenuType.OptionsMenu:
                optionsMenu.SetActive(false);
                break;
            case MenuType.HUDMenu:
                hudMenu.SetActive(false);
                break;
            case MenuType.SceneLoadMenu:
                sceneLoadMenu.SetActive(false);
                break;
        }
    }

    internal void ShowMenu(MenuType menuType)
    {
        switch (menuType)
        {
            case MenuType.MainMenu:
                mainMenu.SetActive(true);
                break;
            case MenuType.PauseMenu:
                pauseMenu.SetActive(true);
                break;
            case MenuType.OptionsMenu:
                optionsMenu.SetActive(true);
                break;
            case MenuType.HUDMenu:
                hudMenu.SetActive(true);
                break;
            case MenuType.SceneLoadMenu:
                sceneLoadMenu.SetActive(true);
                break;
        }
    }
}
