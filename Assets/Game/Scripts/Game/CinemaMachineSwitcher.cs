using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CinemaMachineSwitcher : MonoBehaviour
{
    [Header("Callbacks")]
    public UnityEvent onEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterBase player))
        {
            OnEnter();
        }
    }

    private void OnEnter()
    {
        onEnter.Invoke();
        //Set this to false when you want to switch back to 3rd Person
<<<<<<< HEAD
        GameManager.Instance.bUsingStaticCam = true;
=======
        GameManager.Instance.playerRef.UpdateOrientation();
>>>>>>> Developing
    }

}


