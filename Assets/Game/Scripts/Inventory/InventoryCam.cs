using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCam : MonoBehaviour
{
    [SerializeField] float camMoveSpeed = 100f;
    
    //How much the camera should move vertically
    [SerializeField] int lowerLimit = -45;
    [SerializeField] int upperLimit = 45; 
    
    private float xAngle = 0;
    private float yAngle = 0;

    [Header("Camera Settings for Inventory")]
    [SerializeField] Transform invCam;

    public float zoomSpeed = 10f;      // Speed at which the camera zooms
    public float minZoom = 1f;         // Minimum distance (zoom in limit)
    public float maxZoom = 50f;        // Maximum distance (zoom out limit)

    private float currentZoom;

    private void Start()
    {
        invCam = transform.GetComponentInChildren<Camera>().transform;

        currentZoom = invCam.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"I am working!!!! {Time.deltaTime} {Time.unscaledDeltaTime}");

        //if (Input.GetKeyDown(KeyCode.P))
        //    Time.timeScale = Time.timeScale == 1f ? 0f : 1f;

        if(Input.GetMouseButton(0))
        {
            float movX = GameManager.Instance.playerRef.lookInput.x;
            float movY = GameManager.Instance.playerRef.lookInput.y;

            xAngle += movY * camMoveSpeed * Time.unscaledDeltaTime;
            xAngle = Mathf.Clamp(xAngle, lowerLimit, upperLimit);

            float yAngle = movX * camMoveSpeed * Time.unscaledDeltaTime;

            transform.rotation = Quaternion.Euler(-xAngle, transform.rotation.eulerAngles.y + yAngle, 0f);

        }

        float zoomAmount = GameManager.Instance.playerRef.zoom;

        // Adjust the currentZoom based on input and speed
        currentZoom += zoomAmount * zoomSpeed * Time.unscaledDeltaTime;

        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        
        invCam.localPosition = new Vector3(invCam.localPosition.x, invCam.localPosition.y, currentZoom);
    }
}
