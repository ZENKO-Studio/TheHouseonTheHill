using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HudDialogueTrigger : MonoBehaviour
{
    [Tooltip("Should disable after one time?")]
    [SerializeField] bool bOneUse = true;

    [Tooltip("How long should the text be visible")]
    [SerializeField] int duration = 5;

    [Tooltip("What text should be shown")]
    [SerializeField] string displayText;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.playerHud.UpdateDialogueText(displayText, duration);
        if(bOneUse)
            GetComponent<BoxCollider>().enabled = false;
    }
}
