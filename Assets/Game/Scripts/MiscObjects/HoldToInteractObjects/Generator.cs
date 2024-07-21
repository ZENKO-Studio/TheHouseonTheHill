using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : HoldInteractable
{
    [SerializeField] List<GameObject> lights;

    protected override void Start()
    {
        base.Start();

        //Our Code
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
        }
    }

    protected override void OnInteractionComplete()
    {
        //Our Code
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
        }

        base.OnInteractionComplete();
    }
}
