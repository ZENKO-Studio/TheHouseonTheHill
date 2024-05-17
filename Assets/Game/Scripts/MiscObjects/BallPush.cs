using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPush : MonoBehaviour
{
    public float forceMagnitude = 10f;
    public Vector3 forceDirection = Vector3.right; // Change the direction as per your requirement

    private bool hasFallen = false;
    private Rigidbody sphereRigidbody;

    public AudioClip audioClip; // Audio clip to play
    private AudioSource audioSource;
    private bool hasPlayed = false;

    private void Start()
    {
        // Get the Rigidbody component
        sphereRigidbody = GetComponentInChildren<Rigidbody>();
        // Disable the Rigidbody initially
        sphereRigidbody.isKinematic = true;
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If there's no AudioSource component, add one
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the audio clip to the AudioSource
        audioSource.clip = audioClip;

        // Ensure the audio only plays once
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasFallen && !hasPlayed)
        {
            // Activate the Rigidbody
            sphereRigidbody.isKinematic = false;
            // Apply force in the specified direction
            sphereRigidbody.AddForce(forceDirection.normalized * forceMagnitude, ForceMode.Impulse);
            hasFallen = true;
            audioSource.Play();
            hasPlayed = true; // Set hasPlayed to true so it doesn't play again
        }
    }
}
