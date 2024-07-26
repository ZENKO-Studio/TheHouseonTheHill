using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Dial : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float rayDistance = 100f;
    public LayerMask layer;
    public string targetTag = "RotatableDial"; // Tag to identify specific dials

    public UnityEvent<char> OnDialRotated; 
    
    private float _rotation;

    private void Start()
    {
        transform.localEulerAngles = new Vector3(_rotation, -90, -90);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RotateDialButSlightlyDifferently();
        }
    }



    private void RotateDialButSlightlyDifferently()
    {
        Input.GetMouseButtonDown(0);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, rayDistance, layer))
        {
            // Check if the hit object has the specified tag
            if (hit.collider.gameObject == gameObject)
            {
                _rotation += 360f / 26;
                // Normalize the angle to stay within 0 to 360 degrees
                _rotation = _rotation % 360;
                // Update the rotation of the dial
                transform.localEulerAngles = new Vector3(_rotation, -90, -90);

                // Map the rotation to the range 0-25
                int letterIndex = Mathf.FloorToInt(_rotation / 360f * 26);
                char letter = (char)('A' + letterIndex);

                //Debug.Log("Dial rotated to letter: " + letter);
                OnDialRotated?.Invoke(letter); 
            }
        }
    }



}
