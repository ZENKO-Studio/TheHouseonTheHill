using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Scripts.MiscObjects.DoorController.cs
{
    public class DoorController : MonoBehaviour
    {
        public Animator doorAnimator; // Reference to the animator component of the door
        public KeyCode interactKey = KeyCode.E; // The key to press to interact with the door
        public AudioSource doorAudioSource;
        private bool playerInRange = false; // Flag to track if the player is in range

        void Update()
        {
            // Check if the player is in range and pressing the interact key
            if (playerInRange && Input.GetKeyDown(interactKey))
            {
                // Trigger the "Open" animation if it's not already playing
                if (!doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Door Animation"))
                {
                    doorAnimator.SetTrigger("Open");
                }
                doorAudioSource.Stop();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            // Check if the object entering the trigger is tagged as "Player"
            if (other.CompareTag("Player"))
            {
                // Set playerInRange flag to true
                playerInRange = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            // Check if the object exiting the trigger is tagged as "Player"
            if (other.CompareTag("Player"))
            {
                // Set playerInRange flag to false
                playerInRange = false;
            }
        }
    }
}