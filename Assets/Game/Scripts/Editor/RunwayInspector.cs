using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Runway))]
public class RunwayInspector : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Get the target script
        Runway rw = (Runway)target;

        // Add a button to destroy children
        if (GUILayout.Button("Generate"))
        {
            rw.DestroyAllChildren();
            rw.GenerateRunway();
        }

        if (GUILayout.Button("Clear Runways"))
        {
            rw.DestroyAllChildren();
        }
    }
}