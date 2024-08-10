using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimSequenceTrigger : MonoBehaviour
{
    [SerializeField] bool bShouldDisablePlayerControl = false;

    [SerializeField] GameObject animationObject;

    Animator sequenceAnimator;

    PlayableDirector sequenceTimeline;

    bool bTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!animationObject)
            Debug.LogError($"{name} requires an animation sequence to trigger");

        if(animationObject.TryGetComponent<Animator>(out sequenceAnimator))
            sequenceAnimator.enabled = false;
        else if(animationObject.TryGetComponent<PlayableDirector>(out sequenceTimeline))
            sequenceTimeline.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerSequence();
    }

    internal void TriggerSequence()
    {
        if (animationObject != null && !bTriggered)
        {
            if (sequenceAnimator)
            {
                sequenceAnimator.enabled = true;
            }
            else if (sequenceTimeline)
            {
                sequenceTimeline.enabled = true;
            }
            bTriggered = true;
            if (bShouldDisablePlayerControl)
            {
                GameManager.Instance.playerRef.SetPlayerHasControl(false);

                if (sequenceAnimator)
                {
                    Invoke(nameof(ResetPlayerControl), sequenceAnimator.GetCurrentAnimatorStateInfo(0).length);//
                }
                else if (sequenceTimeline)
                {
                    Invoke(nameof(ResetPlayerControl), (float)sequenceTimeline.duration);//
                }
            }
        }
    }

    void ResetPlayerControl()
    {
        GameManager.Instance.playerRef.SetPlayerHasControl(true);
    }
}
