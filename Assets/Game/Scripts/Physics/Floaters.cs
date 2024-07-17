using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

/*
 * Floaters are used to attach to points on an object to simulate buoyancy. Create an empty GameObject add this script
 * to it and choose the points on the object you would like it to "float" from.
 *
 * @Author Brandon Bennie
 * 
 */


[RequireComponent(typeof(Rigidbody))]
    
public class Floaters : MonoBehaviour
{

    // Rigid-body component of floating objects
    public Rigidbody rb;

    //Depth at which objects Experince bouancy
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
    private WaterSearchResult _searchResults;

    private void FixedUpdate()
    {
        //Apply a distributed gravitational force
        rb.AddForceAtPosition(Physics.gravity / floaters, transform.position, ForceMode.Acceleration);
        
        //Set up Search parameters for projecting on water surface
        _search.startPositionWS = transform.position;
        
        //project point onto water surface and get result
        water.ProjectPointOnWaterSurface(_search, out _searchResults);

        //If object is below water surface
        if (transform.position.y < _searchResults.projectedPositionWS.y)
        {

            //Calculate displacement multiplier based on submersion depth
            float displacementMulti =
                Mathf.Clamp01((_searchResults.projectedPositionWS.y - transform.position.y) / depthBefSub) *
                displacementAmt;
            
            //Apply buoyant force upwards
            rb.AddForceAtPosition(new Vector3(0f,Mathf.Abs(Physics.gravity.y)* displacementMulti , 0f), transform.position, ForceMode.Acceleration);
            
            //Apply water drag force against velocity
            rb.AddForce(-rb.velocity * (displacementMulti * waterDrag * Time.fixedDeltaTime), ForceMode.VelocityChange);
            
            //Apply water angular drag torque against angular velocity
            rb.AddTorque(-rb.angularVelocity * (displacementMulti * waterAngularDrag * Time.fixedDeltaTime), ForceMode.VelocityChange);


            
        }

    }

   
}
