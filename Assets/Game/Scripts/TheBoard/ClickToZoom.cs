using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    CinemachineComponentBase componentBase;

    float cameraDistance;

    [SerializeField] float Sensitivity = 10f;


    private void Update()
    {

        if (componentBase == null) {
        
            componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);


        
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        { 
        
            cameraDistance = Input.GetAxis("Mouse ScrollWheel") * Sensitivity;
            if (componentBase is CinemachineFramingTransposer)
            { 
            
                (componentBase as CinemachineFramingTransposer).m_CameraDistance = cameraDistance;

            }

        }


    }


}

