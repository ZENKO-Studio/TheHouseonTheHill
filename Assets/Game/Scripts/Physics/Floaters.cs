using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Floaters : MonoBehaviour
{

    // Rigid-body component of floating objects
    public Rigidbody rb;

    //Depth at which objects Experice bouancy
    [SerializeField,Range(0,10)]public float depthBefSub;
    //Amount of buoyant force applied
    [SerializeField, Range(0, 10)] public float displacementAmt;
   //Number of points applying buoyant force
    public int floaters;


    //Drag coefficient in water
    [SerializeField, Range(0, 100)] public float waterDrag;
    //Angular Drag coefficient in water
    [SerializeField, Range(0, 10)] public float waterAngularDrag;
    //Reference to the water surface management component
    public WaterSurface water;

    private WaterSearchParameters _search;
    private WaterSearchParameters _searchResults;

    private void FixedUpdate()
    {
        
        rb.AddForceAtPosition(Physics.gravity / floaters, transform.position, ForceMode.Acceleration);
        _search.startPosition = transform.position;
        water.


    }

   
}
