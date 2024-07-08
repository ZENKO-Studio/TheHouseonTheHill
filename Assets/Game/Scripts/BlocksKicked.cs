using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksKicked : MonoBehaviour
{
    public float kickForce = 500;
    public float interactionDistance = 2.0f;  // This can be used if needed for further checks
    public float interactionAngle = 45.0f;
    private GameObject player;
    public AudioSource audioSource;
    //public List<Rigidbody> interactableBlocks = new List<Rigidbody>(); // Store interactable blocks
    public Rigidbody rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block")) // Make sure your blocks have the tag "Block"
        {
            rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply force to simulate the kick
                rb.AddForce(transform.forward * 500); // Adjust force as necessary




                audioSource = other.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.Play();



                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //interactableBlocks.Remove(rb);
            }
        }
    }

    void TryInteract()
    {
        
            if (IsFacingBlock(rb.transform))
            {
                if (IsPlayerSprinting())
                {
                    KickBlock(rb);
                }
                else
                {
                    InteractWithBlock(rb);
                }
            }
        
    }

    bool IsFacingBlock(Transform blockTransform)
    {
        Vector3 directionToBlock = blockTransform.position - player.transform.position;
        float angle = Vector3.Angle(player.transform.forward, directionToBlock);
        return angle < interactionAngle;
    }

    bool IsPlayerSprinting()
    {
        // Replace this method with the actual check for whether the player is sprinting
        // This is a placeholder to demonstrate the logic.
        //return player.GetComponent<StarterAssets.ThirdPersonController>().MoveSpeed > 1;
        return true;
    }

    void KickBlock(Rigidbody blockRigidbody)
    {
        blockRigidbody.AddForce(player.transform.forward * kickForce, ForceMode.Impulse);
        PlayAudio();
    }

    void InteractWithBlock(Rigidbody blockRigidbody)
    {
        // Implement custom interaction logic here
        Debug.Log("Interacted with block: " + blockRigidbody.gameObject.name);
        PlayAudio();
    }

    void PlayAudio()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
            Sound s = new Sound(transform.position, 20f);
            Sounds.MakeSound(s);
        }
    }
}
