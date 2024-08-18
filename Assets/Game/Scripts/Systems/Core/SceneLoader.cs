using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] private SceneReference mainMenuScene;

    //This is where all the load and unload operations will take place
    private List<AsyncOperation> sceneLoadOperations = new List<AsyncOperation>();

    private List<SceneReference> loadedGameScenes = new List<SceneReference>();


    private bool bLoadInProgress = false;

    public float LoadProgress { get; private set; }

    public void LoadScene(SceneReference sceneReference)
    {
        MenuManager.Instance.ShowMenu(MenuType.SceneLoadMenu);

        sceneLoadOperations.Add(SceneManager.LoadSceneAsync(sceneReference, LoadSceneMode.Additive));
        loadedGameScenes.Add(sceneReference);
        
        if (!bLoadInProgress)
            StartCoroutine(nameof(StartLoading));
    }

    public void UnloadScene(SceneReference sceneReference)
    {
        MenuManager.Instance.ShowMenu(MenuType.SceneLoadMenu);

        sceneLoadOperations.Add(SceneManager.UnloadSceneAsync(sceneReference));
        loadedGameScenes.Remove(sceneReference);

        if (!bLoadInProgress)
            StartCoroutine(nameof(StartLoading));
    }

    public IEnumerator StartLoading()
    {
        bLoadInProgress = true;

        float operationProgress = 0f;

        for (int i = 0; i < sceneLoadOperations.Count; i++)
        {
            while(!sceneLoadOperations[i].isDone)
            {
                operationProgress += sceneLoadOperations[i].progress;
                LoadProgress = Mathf.Clamp01((operationProgress / sceneLoadOperations.Count) / .9f);
                yield return null;
            }
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadedGameScenes[loadedGameScenes.Count - 1].SceneName));
        MenuManager.Instance.HideMenu(MenuType.SceneLoadMenu);

        LoadProgress = 0f;
        bLoadInProgress = false;
        GameManager.Instance.loadInProgress = false;
    }

    public void ReloadMainMenu()
    {
        MenuManager.Instance.ShowMenu(MenuType.SceneLoadMenu);

        //Unload all the active game scenes (Game Levels)
        foreach (SceneReference sceneReference in loadedGameScenes)
        {
            sceneLoadOperations.Add(SceneManager.UnloadSceneAsync(sceneReference));
        }
        sceneLoadOperations.Clear();
        loadedGameScenes.Clear();

        sceneLoadOperations.Add(SceneManager.LoadSceneAsync(mainMenuScene, LoadSceneMode.Additive));
        loadedGameScenes.Add(mainMenuScene);

        Time.timeScale = 1f;

        if (!bLoadInProgress)
            StartCoroutine(nameof(StartLoading));

        GameManager.Instance.currentGameState = GameState.MainMenu;
    }

}
