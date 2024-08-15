using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    CutscenePlaying,    //Optional
    GameRunning,
    GamePaused,
    GameEnded
}

public class GameManager : Singleton<GameManager>
{
    #region Core Gameplay Prefabs References

    [Tooltip("Reference to the Nell`s Prefab")]
    [SerializeField] GameObject NellPrefab;
    
    [Tooltip("Reference to the Inventory System Prefab")]
    [SerializeField] GameObject InventorySystemPrefab;
    
    [Tooltip("Reference to the HUD prefab")]
    [SerializeField] GameObject HUDPrefab;

    #endregion

    [Tooltip("Set your first game level here, one to be loaded on start game")]
    public SceneReference FirstGameLevel;

    [Tooltip("Set your second game level here, one to be loaded on level 2 btn")]
    public SceneReference SecondGameLevel;

    //Should be set on game start or manually in the scene
    public NellController playerRef;

    public HUDController playerHud;

    public UnityEvent OnPlayerSpawned = new UnityEvent();

    public UnityEvent OnGameResumed = new UnityEvent();

    //When Game Starts
    internal GameState currentGameState = GameState.MainMenu;

    #region Cam View and Player Movement Orientation
    //Can be used for movable objects since it requires some transform to base direction off
    [Tooltip("Should be set when orientation needs to be overridden (Set it to null when using Third Person Camera)")]
    Transform overridenOrientation = null;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SceneLoader.Instance.ReloadMainMenu();
    }

    #region Core Game Functions
    public void StartGame()
    {
        if (FirstGameLevel != null)
        {
            SceneLoader.Instance.LoadScene(FirstGameLevel);
            StartCoroutine(nameof(SceneLoader.Instance.StartLoading));
        }
        else
        {
            Debug.LogError($"Game Manager Script on {name} needs valid scene reference to load");
        }
    }

    public void StartLevel(int l)
    {
        if (l == 1)
        {
            if (FirstGameLevel != null)
            {
                SceneLoader.Instance.LoadScene(FirstGameLevel);
            }
            else
            {
                Debug.LogError($"Game Manager Script on {name} needs valid scene reference to load");
            }
        }

        else if (l == 2)
        {
            if (SecondGameLevel != null)
            {
                SceneLoader.Instance.LoadScene(SecondGameLevel);
            }
            else
            {
                Debug.LogError($"Game Manager Script on {name} needs valid scene reference to load");
            }
        }

        GameManager.Instance.currentGameState = GameState.GameRunning;
    }

    public void PauseGame(bool bShowPauseScreen)
    {
        Time.timeScale = 0f;

        if (bShowPauseScreen)
        {
            MenuManager.Instance.ShowMenu(MenuType.PauseMenu);
        }

        MenuManager.Instance.HideMenu(MenuType.HUDMenu);

        currentGameState = GameState.GamePaused;
    }

    public void ResumeGame()
    {
        OnGameResumed?.Invoke();

        Time.timeScale = 1f;
      
        MenuManager.Instance.HideMenu(MenuType.PauseMenu);
        MenuManager.Instance.ShowMenu(MenuType.HUDMenu);

        currentGameState = GameState.GameRunning;
    }

    //Got to the Main Menu Screen 
    public void EndGame()
    {
        SceneLoader.Instance.ReloadMainMenu();
    }
    #endregion

    //To be called when game scene is loaded
    void SetupCoreComponents()
    {
        #region Ensure all the core components are setup
        if (playerRef == null)
        {
            Debug.LogError($"Please Setup Nell Prefab in {name}"); 
            return;
        }
        
        if (HUDPrefab == null)
        {
            Debug.LogError($"Please Setup HUD Prefab in {name}"); 
            return;
        }

        if (InventorySystemPrefab == null)
        {
            Debug.LogError($"Please Setup Inventory System Prefab in {name}");
            return;
        }
        #endregion

        #region Spawn all the stuff

        //Player 
        //Get this from check point system
        Transform checkpointTrans = null;

        playerRef = Instantiate(NellPrefab).GetComponent<NellController>();
        //playerRef.transform.position = checkpointTrans.position;
        //playerRef.transform.rotation = checkpointTrans.rotation;

        //HUD
        Instantiate(HUDPrefab);

        //Inventory System
        Instantiate(InventorySystemPrefab);

        #endregion
    }

    //Get Active Camera
    public Transform OverriddenOrientation()
    {
        return overridenOrientation; 
    }

    //Call this method when overriding player orientation
    public void OverrideOrientation(Transform overridenTransform)
    {
        this.overridenOrientation = overridenTransform;
        playerRef.UpdateOrientation();
    }

    private void OnApplicationFocus(bool focus)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void PlayerSpawned(NellController player)
    {
        playerRef = player;

        OnPlayerSpawned?.Invoke();
    }
}
