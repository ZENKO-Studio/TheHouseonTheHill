using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Dial : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float rayDistance;
    public LayerMask layer;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RotateDial();
        }
    }

    private void RotateDial()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, layer))
        {
            if (!hit.collider.CompareTag("RotatableDial")) return;
            
            // If the ray hits an object, log the name of the object
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            // You can also access other information about the hit object, like its position
            Debug.Log("Hit position: " + hit.point);

        }
    }

    public void Rotate()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
