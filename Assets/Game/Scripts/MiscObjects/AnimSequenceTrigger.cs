using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSequenceTrigger : MonoBehaviour
{
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (!animator)
            Debug.LogError($"{name} requires an animation sequence to trigger");

        animator.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (animator != null)
        {
            animator.enabled = true;
        }
    }
}
