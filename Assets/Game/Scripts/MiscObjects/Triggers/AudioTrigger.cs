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
            AudioSource.PlayClipAtPoint(clipToPlay, transform.position);
        }

        if(bOneUse)
            GetComponent<BoxCollider>().enabled = false;
    }
}
