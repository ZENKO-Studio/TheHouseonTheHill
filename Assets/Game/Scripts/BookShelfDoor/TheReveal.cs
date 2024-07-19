using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Interactable;
using UnityEngine;
using UnityEngine.Events;

public class TheReveal : MonoBehaviour
{

    public OpenThings _door;
    
    public UnityEvent onSomething;
    
    // Update is called once per frame
    void Update()
    {

        if (_door.IsOpen)
        {
            Swing();
        }

    }

    public void Swing()
    {

        onSomething?.Invoke();

    }

}
