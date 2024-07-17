using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour
{
    public List<Dial> dials; // List of Dial script
    public string targetCombination;

    void Start()
    {
  
        SetCombination(targetCombination);
    }

    public void SetCombination(string combination)
    {
        for (int i = 0; i < combination.Length && i < dials.Count; i++)
        {
            int letterIndex = combination[i] - 'A'; // Convert letter to index (A=0, B=1, ..., Z=25)
            float targetAngle = 360f / 26 * letterIndex; // Calculate the angle
            
        }
    }
}
