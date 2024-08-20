using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnTrigger : MonoBehaviour
{
    public AudioClip audioClip; // The audio clip you want to play
    private AudioSource audioSource; // The AudioSource component

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            // If there's no AudioSource component, add one
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the audio clip to the audio source
        audioSource.clip = audioClip;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has a specific tag (optional)
        // if (other.CompareTag("Player")) // Uncomment this line if you want to check for a specific tag

        // Play the audio clip when the trigger is hit
        if (audioSource != null && audioClip != null)
        {
            audioSource.Play();
            Debug.Log("Audio played on trigger.");
        }
    }
}
