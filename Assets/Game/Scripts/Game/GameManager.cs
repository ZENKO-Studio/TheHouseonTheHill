using UnityEngine;

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


    //Should be set on game start or manually in the scene
    [HideInInspector] public NellController playerRef;

    #region Cam View and Player Movement Orientation
    //Can be used for movable objects since it requires some transform to base direction off
    [Tooltip("Should be set when camera switches (Set it to null when using Third Person Camera)")]
    Transform activeCamTransform = null;
    
    public bool bUsingStaticCam;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        playerRef.transform.position = checkpointTrans.position;
        playerRef.transform.rotation = checkpointTrans.rotation;

        //HUD
        Instantiate(HUDPrefab);

        //Inventory System
        Instantiate(InventorySystemPrefab);

        #endregion
    }

    //Get Active Camera
    public Transform ActiveCam()
    {
        return activeCamTransform; 
    }

    //Call this method when switching camera
    public void SetActiveCamTransform(Transform activeCamTransform)
    {
        this.activeCamTransform = activeCamTransform;
    }

    private void OnApplicationFocus(bool focus)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

}
