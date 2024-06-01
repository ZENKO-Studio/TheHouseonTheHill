using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimators;
    public float delayTime = 3.0f; // The amount of time to wait before playing the animation

    void Start()
    {
        if (doorAnimators == null)
        {
            doorAnimators = GetComponent<Animator>();
        }

        StartCoroutine(PlayDoorAnimationAfterDelay());
    }

    IEnumerator PlayDoorAnimationAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        doorAnimators.SetTrigger("OpenDoor"); // Assuming you have a trigger parameter named "OpenDoor" in your Animator
    }
}
