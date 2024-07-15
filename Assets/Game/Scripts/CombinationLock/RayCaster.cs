using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        ManageInput();

    }

    private void ManageInput()
    {
        if (Input.GetMouseButtonDown(0))
            RayCast();

    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void RayCast()
    {

        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 5);
        
        if(hit.collider == null) return;

        Dial dial = hit.collider.GetComponent<Dial>();
        
        if (dial == null) return;
        
        dial.Rotating();


    }
}
