/**@Sami 06/20/24
 * This class just holds the referenc to the CinemachineStateDrivenCamera so that it can hold active camera
 */

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineStateCamRef : Singleton<CinemachineStateCamRef>
{
    public CinemachineStateDrivenCamera stateDrivenCamera;
    public Transform activeCamTransform;

    // Start is called before the first frame update
    void Start()
    {
        stateDrivenCamera = GetComponent<CinemachineStateDrivenCamera>();
    }

    public void SetActiveCam()
    {
        activeCamTransform = stateDrivenCamera.LiveChild.VirtualCameraGameObject.transform;
        Debug.Log($"Active Cam {activeCamTransform.name}");
<<<<<<< HEAD
        GameManager.Instance.SetActiveCamTransform( activeCamTransform );
=======
        GameManager.Instance.OverrideOrientation( activeCamTransform );
>>>>>>> Developing
    }

}
