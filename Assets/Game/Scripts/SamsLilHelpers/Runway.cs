using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Runway : MonoBehaviour
{
    [SerializeField] GameObject runwayBlock;

    [SerializeField] Material mOdd;
    [SerializeField] Material mEven;

    [SerializeField] Material mFifth;

    [SerializeField] int length = 10;

    [SerializeField] int noRunways = 2;

    [SerializeField] int distBetweenRunways = 2;


    public void DestroyAllChildren()
    {
        // Destroy all child objects
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    public void GenerateRunway()
    {
        for (int i = 0; i < length; i++)
        {
            for(int j = 0; j < noRunways; j++)
            {
                GameObject g = Instantiate(runwayBlock);
                g.transform.parent = transform;
                g.transform.localPosition = new Vector3(j * distBetweenRunways + 1, 0, i);

                if((i + 1) % 2 == 0)
                    g.GetComponentInChildren<Renderer>().material = mEven;
                else
                    g.GetComponentInChildren<Renderer>().material = mOdd;

                if((i + 1) % 5 == 0)
                    g.GetComponentInChildren<Renderer>().material = mFifth;

                g.GetComponentInChildren<TMP_Text>().text = $"{i + 1}";
            }
        }
    }
}
