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
<<<<<<< HEAD
            GameManager.Instance.SetActiveCamTransform(transform);
=======
            GameManager.Instance.OverrideOrientation(transform);
>>>>>>> Developing
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
<<<<<<< HEAD
            GameManager.Instance.SetActiveCamTransform(null);
=======
            GameManager.Instance.OverrideOrientation(null);
>>>>>>> Developing
        }
    }
}
