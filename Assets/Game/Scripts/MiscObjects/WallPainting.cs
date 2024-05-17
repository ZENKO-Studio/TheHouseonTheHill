using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPainting : MonoBehaviour
{
    public Animator animator;
    private bool playerInRange = false;
    private bool animationPlayed = false;
    public AudioSource wallPainting;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !animationPlayed)
        {
            PlayAnimation();
        }

        if (animationPlayed)
        {
            Invoke("ResetAnimation", 3f);
        }
    }

    void PlayAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("FixPainting");
            wallPainting.Play();
            animationPlayed = true;
        }
    }

    void ResetAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("NotFixPainting");
            //animator.speed = -1f;
            //animator.Play("Wall Painting Anim", 0, 1); // Play from the end
            animationPlayed = false;
        }
    }
}
