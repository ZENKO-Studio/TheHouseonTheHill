using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : HoldInteractable
{
    [SerializeField] List<GameObject> lights;

    [SerializeField] int soundRange = 10;

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
        if(interactedTime > 0 && interactedTime % 1 == 0)
        {
            var sound = new Sound(transform.position, soundRange);

            Sounds.MakeSound(sound);
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
