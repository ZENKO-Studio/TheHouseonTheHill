using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
   
    //[SerializeField] private AudioSource source = null;

    [SerializeField] private float soundRange = 25f;

    [SerializeField] private int loudness = 0;

    private void OnMouseDown()
    {
        var sound = new Sound(transform.position, soundRange);

        Sounds.MakeSound(sound);
    }
    
}
