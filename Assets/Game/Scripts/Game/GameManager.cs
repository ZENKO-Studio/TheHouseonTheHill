using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //Should be set on game start or manually in the scene
    public NellController playerRef;

    //Should be set when camera switches (Set it to null when using Third Person Camera)
    Transform activeCamTransform = null;
    
    public bool bUsingStaticCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
