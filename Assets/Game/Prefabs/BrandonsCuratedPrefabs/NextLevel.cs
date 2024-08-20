using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : StateMachineBehaviour
{


    [SerializeField] public int Level;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // This method gets triggered when the animation ends
        Debug.Log("Animation has ended");

        // Call the GameManager to start the next level, if needed
        GameManager.Instance.StartLevel(Level); // Example: loads level 1
    }
}
