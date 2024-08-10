using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSwitch : InteractableObject
{
    [SerializeField] Generator generatorRef;

    public override void Interact()
    {
        if (generatorRef != null)
        {
            generatorRef.ActivateSwitch();
        }
    }
}
