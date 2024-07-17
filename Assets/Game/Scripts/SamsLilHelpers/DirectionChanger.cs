using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChanger : MonoBehaviour
{
    CinemachineStateDrivenCamera cam;

    private void OnTriggerEnter(Collider other)
    {
        //cam.LiveChild;

        if(other.CompareTag("Player"))
        {
            GameManager.Instance.OverrideOrientation(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.OverrideOrientation(null);
        }
    }
}
