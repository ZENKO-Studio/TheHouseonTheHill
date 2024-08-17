using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AudioTrigger : MonoBehaviour
{
    [Tooltip("Should disable after one time?")]
    [SerializeField] bool bOneUse = true;

    [Tooltip("The audio clip corresponding to the dialogue")]
    [SerializeField] AudioClip clipToPlay;
    
    [Tooltip("The audio clip volume for the dialogue")]
    [SerializeField] [Range(0,1)] float volume;

    
    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if(clipToPlay != null )
        {
            AudioSource.PlayClipAtPoint(clipToPlay, transform.position, volume);
        }

        if(bOneUse)
            GetComponent<BoxCollider>().enabled = false;
    }
}
