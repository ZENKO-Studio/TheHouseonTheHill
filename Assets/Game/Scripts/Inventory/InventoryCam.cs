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
    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"I am working!!!! {Time.deltaTime} {Time.unscaledDeltaTime}");

        //if (Input.GetKeyDown(KeyCode.P))
        //    Time.timeScale = Time.timeScale == 1f ? 0f : 1f;

        if(Input.GetMouseButton(0))
        {
            float movX = Input.GetAxis("Mouse X");
            float movY = Input.GetAxis("Mouse Y");

            xAngle += movY * camMoveSpeed * Time.unscaledDeltaTime;
            xAngle = Mathf.Clamp(xAngle, lowerLimit, upperLimit);

            float yAngle = movX * camMoveSpeed * Time.unscaledDeltaTime;

            transform.rotation = Quaternion.Euler(-xAngle, transform.rotation.eulerAngles.y + yAngle, 0f);
            //transform.Rotate(0, , 0);
        }
    }
}
