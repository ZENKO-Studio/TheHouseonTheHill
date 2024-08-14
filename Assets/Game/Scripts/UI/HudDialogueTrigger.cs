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

    [Tooltip("The audio clip corresponding to the dialogue")]
    [SerializeField] AudioClip clipToPlay;

    
    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameManager.Instance.playerHud.UpdateDialogueText(displayText, duration);
        
        if(clipToPlay != null )
        {
            AudioSource.PlayClipAtPoint(clipToPlay, transform.position);
        }

        if(bOneUse)
            GetComponent<BoxCollider>().enabled = false;
    }
}
