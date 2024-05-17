using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Stalker))]
public class StalkerInspector : Editor
{
    

    bool bOnSrc = true;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Get the target script
        Stalker stalkerRef = (Stalker)target;

        // Add a button to destroy children
        if (GUILayout.Button("Move"))
        {
            stalkerRef.stalkerAgent.speed = stalkerRef.moveSpeed;
            stalkerRef.stalkerAgent.SetDestination(bOnSrc ? stalkerRef.dest1.position : stalkerRef.dest2.position);
            bOnSrc = !bOnSrc;
        }

        if (GUILayout.Button("Chase"))
        {
            stalkerRef.stalkerAgent.speed = stalkerRef.chaseSpeed;
            stalkerRef.stalkerAgent.SetDestination(bOnSrc ? stalkerRef.dest1.position : stalkerRef.dest2.position);
            bOnSrc = !bOnSrc;
        }
    }
}