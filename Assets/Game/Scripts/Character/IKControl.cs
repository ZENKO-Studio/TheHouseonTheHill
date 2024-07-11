using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class IKControl : MonoBehaviour
{
    protected Animator animator;

    public bool ikActive = false;
    public Transform trackObj = null;
    public Transform lookObj = null;
    public float ikWeight = 0.0f;
    [SerializeField] private AvatarIKGoal ikGoal;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal.
            if (ikActive)
            {

                // Set the look target position, if one has been assigned
                if (lookObj != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if ( trackObj != null)
                {
                    animator.SetIKPositionWeight(ikGoal, 1);
                    animator.SetIKRotationWeight(ikGoal, 1);
                    animator.SetIKPosition(ikGoal, trackObj.position);
                    animator.SetIKRotation(ikGoal, trackObj.rotation);
                }

            }
        }
    }
}