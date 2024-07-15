using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Dial : MonoBehaviour
{

    [Header("Settings")] 
    [SerializeField] private float animDuration;

    private bool isRotating = false;
    private int currentIndex;

    [Header("Events")] [SerializeField] private UnityEvent<Dial> onDialRotated;

    private void Start()
    {
        currentIndex = Random.Range(0, 26);
        transform.localRotation = Quaternion.Euler(currentIndex * -36,0,0);
    }

    public void Rotating()
    {
        if(isRotating) return;

        isRotating = true;

        currentIndex++;

        if (currentIndex >= 26)
            currentIndex = 0;


    }

    private void RotationCompleteCallBack()
    {
        
        onDialRotated?.Invoke(this);
        
    }

    public int GetNumber()
    {

        return currentIndex;
    }

    public void Lock()
    {

        isRotating = true;

    }

    public void Unlock()
    {

        isRotating = false;


    }

}
