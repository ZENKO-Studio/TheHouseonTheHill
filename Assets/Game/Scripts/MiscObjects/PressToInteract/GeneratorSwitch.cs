using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSwitch : InteractableObject
{
    [SerializeField] Generator generatorRef;
    [SerializeField] GameObject generatorlight;
    [SerializeField] AudioClip leverOnSound;
    public override void Interact()
    {
        if (generatorRef != null)
        {
            generatorRef.ActivateSwitch();
            this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            generatorlight.SetActive(true);
            if (leverOnSound)
                AudioSource.PlayClipAtPoint(leverOnSound, transform.position);
        }
    }
}
