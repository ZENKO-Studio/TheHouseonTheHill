using Cinemachine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera[] secondaryCameras;

    private void Start()
    {
        SwitchToCamera(primaryCamera);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Keypad1))
        {
            SwitchToCamera(primaryCamera);
        }

        for (int i = 0; i < secondaryCameras.Length; i++)
        {
            if (Input.GetKey(KeyCode.Keypad2 + i)) // Keypad2 for the first secondary camera, Keypad3 for the second, etc.
            {
                SwitchToCamera(secondaryCameras[i]);
            }
        }
    }

    private void SwitchToCamera(CinemachineVirtualCamera targetCamera)
    {
        foreach (CinemachineVirtualCamera camera in secondaryCameras)
        {
            camera.enabled = camera == targetCamera;
        }

        primaryCamera.enabled = (primaryCamera == targetCamera);
    }
}
