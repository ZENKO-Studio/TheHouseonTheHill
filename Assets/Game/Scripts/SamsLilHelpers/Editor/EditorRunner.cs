using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EditModeRunner))]
public class EditorRunnerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector first
        DrawDefaultInspector();

        // Add a button to the inspector
        EditModeRunner myComponent = (EditModeRunner)target;
        if (GUILayout.Button("Create Sub Buttons"))
        {
            // Call the function in the target component when the button is clicked
            myComponent.CreateSubButtons();
        }

        if (GUILayout.Button("Solve Puzzle"))
        {
            // Call the function in the target component when the button is clicked
            myComponent.SolvePuzzle();
        }
    }
}
