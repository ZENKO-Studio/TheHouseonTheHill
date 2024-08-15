using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    //This is where all the load and unload operations will take place
    private List<AsyncOperation> sceneLoadOperations = new List<AsyncOperation>();

    private bool bLoadInProgress = false;

    public float LoadProgress { get; private set; }

    public void LoadScene(SceneReference sceneReference)
    {
        MenuManager.Instance.ShowMenu(MenuType.SceneLoadMenu);

        sceneLoadOperations.Add(SceneManager.LoadSceneAsync(sceneReference, LoadSceneMode.Additive));
        
        if (!bLoadInProgress)
            StartCoroutine(nameof(StartLoading));
    }

    public void UnloadScene(SceneReference sceneReference)
    {
        MenuManager.Instance.ShowMenu(MenuType.SceneLoadMenu);

        sceneLoadOperations.Add(SceneManager.UnloadSceneAsync(sceneReference));

        if (!bLoadInProgress)
            StartCoroutine(nameof(StartLoading));
    }

    public IEnumerator StartLoading()
    {
        bLoadInProgress = true;

        float operationProgress = 0f;

        for (int i = 0; i < sceneLoadOperations.Count; ++i)
        {
            while(!sceneLoadOperations[i].isDone)
            {
                operationProgress += sceneLoadOperations[i].progress;
                LoadProgress = Mathf.Clamp01((operationProgress / sceneLoadOperations.Count) / .9f);
                yield return null;
            }
        }

        MenuManager.Instance.HideMenu(MenuType.SceneLoadMenu);

        LoadProgress = 0f;
        bLoadInProgress = false;
    }

    public void ReloadMainMenu()
    {
        MenuManager.Instance.ShowMenu(MenuType.SceneLoadMenu);

        int c = SceneManager.sceneCount;
        if(c > 1)
        {
            for (int i = c; i > 0; i--)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                sceneLoadOperations.Add(SceneManager.UnloadSceneAsync(scene));
            }
        }

        sceneLoadOperations.Add(SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive));

        if (!bLoadInProgress)
            StartCoroutine(nameof(StartLoading));

    }

}
