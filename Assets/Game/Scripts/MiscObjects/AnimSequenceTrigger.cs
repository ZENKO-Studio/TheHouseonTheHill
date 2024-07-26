using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSequenceTrigger : MonoBehaviour
{
    [SerializeField] bool bShouldDisablePlayerControl = false;

    [SerializeField] Animator animator;

    bool bTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!animator)
            Debug.LogError($"{name} requires an animation sequence to trigger");

        animator.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (animator != null && !bTriggered)
        {
            animator.enabled = true;
            bTriggered = true;
            if(bShouldDisablePlayerControl)
            {
                GameManager.Instance.playerRef.SetPlayerHasControl(false);
                Invoke(nameof(ResetPlayerControl), animator.GetCurrentAnimatorStateInfo(0).length);//
            }
        }
    }

    void ResetPlayerControl()
    {
        GameManager.Instance.playerRef.SetPlayerHasControl(true);
    }
}
