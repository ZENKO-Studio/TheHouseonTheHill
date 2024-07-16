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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RotateDial();
        }
    }

    private void RotateDial()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, layer))
        {
            // Check if the hit object has the specified tag
            if (hit.collider.gameObject.CompareTag(targetTag))
            {
                // If the ray hits an object with the correct tag, log the name of the object
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                hit.collider.gameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                // You can also access other information about the hit object, like its position
                Debug.Log("Hit position: " + hit.point);
            }
        }
    }

    public void Rotate()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
