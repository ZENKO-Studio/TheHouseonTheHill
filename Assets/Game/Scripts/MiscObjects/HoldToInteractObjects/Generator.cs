using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : HoldInteractable
{
    [SerializeField] List<GameObject> lights;

    [SerializeField] int soundRange = 10;

    int c = 1;

    protected override void Start()
    {
        base.Start();

        //Our Code
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    
        //Every One Second Make Sound
        if(interactedTime > c)
        {
            var sound = new Sound(transform.position, soundRange);
            Sounds.MakeSound(sound);
            c++;
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
