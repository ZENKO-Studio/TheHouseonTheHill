using System.Collections;
using UnityEngine;
namespace Game.Sripts.DoorController.cs
{
    public class DoorController : MonoBehaviour
    {
        public Animator doorAnimator;
        public float delayTime = 3.0f; // The amount of time to wait before playing the animation

        void Start()
        {
            if (doorAnimator == null)
            {
                doorAnimator = GetComponent<Animator>();
            }

            StartCoroutine(PlayDoorAnimationAfterDelay());
        }

        IEnumerator PlayDoorAnimationAfterDelay()
        {
            yield return new WaitForSeconds(delayTime);
            doorAnimator.SetTrigger("OpenDoor"); // Assuming you have a trigger parameter named "OpenDoor" in your Animator
        }
    }
}