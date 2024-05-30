using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{
    public void OnInteract(InputValue value)
    {
        Debug.Log($"{name} is Interacting");
    }
}
