using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    //This is where all the load and unload operations will take place
    private List<AsyncOperation> sceneLoadOperations = new List<AsyncOperation>();

    public float LoadProgress { get; private set; }

    public void LoadScene(SceneReference sceneReference)
    {
        sceneLoadOperations.Add(SceneManager.LoadSceneAsync(sceneReference, LoadSceneMode.Additive));
    }

    public void UnloadScene(SceneReference sceneReference)
    {
        sceneLoadOperations.Add(SceneManager.UnloadSceneAsync(sceneReference));
    }

    public IEnumerator StartLoading()
    {
        MenuManager.Instance.ShowMenu(MenuType.SceneLoadMenu);

        for (int i = 0; i < sceneLoadOperations.Count; ++i)
        {
            while(!sceneLoadOperations[i].isDone)
            {
                LoadProgress = sceneLoadOperations[i].progress / sceneLoadOperations.Count;
                yield return null;
            }
        }

        sceneLoadOperations.Clear();

        MenuManager.Instance.HideMenu(MenuType.SceneLoadMenu);
    }

    public void ReloadMainMenu()
    {
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

        StartCoroutine(nameof(StartLoading));

    }

}
