using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerInteract : MonoBehaviour
{
    public UnityEvent OneInteract;

    public void OnTriggerEnter(Collider other)
    {
        OneInteract?.Invoke();
    }
}
