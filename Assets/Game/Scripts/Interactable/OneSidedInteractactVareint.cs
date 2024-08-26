using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Interactable;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class OneSidedInteractactVareint : MonoBehaviour, IInteractable
{
    
    public UnityEvent onInteract;
    [SerializeField] private InputAction interactAction;
    [SerializeField] private int priority;

    public OpenThings openThings;
    
    #region StoleFromSami

    

    
    [Header("Audio and Dialogue")]
    [SerializeField] bool PlayDoorSound = false;
    [SerializeField] bool PlayDialogueLines = false;

    [SerializeField] AudioClip lockedDoorSound; 

    [SerializeField] AudioClip unlockedDoorSound; 

    [Tooltip("Dialogue that will be played once when player enters the trigger and do not have required items")]
    [SerializeField] List<string> linesWhenLocked = new List<string>();

    [Tooltip("Dialogue that will be played once when player enters the trigger have all the items")]
    [SerializeField] List<string> linesWhenUnlocked = new List<string>();
    #endregion


    private void Start()
    {
        openThings = GetComponentInParent<OpenThings>();
        if (openThings == null)
        {
            Debug.LogError("OpenThings component not found on the GameObject.");
        }
    }

    public InputAction Action => interactAction;

    public void Interact(CharacterBase player)
    {
        if (openThings != null && openThings.IsPlayerInFrontVAR(player.transform))
        {
          //e  Gizmos.DrawRay(player.transform.position , openThings.transform.position);
            onInteract?.Invoke();
            if (PlayDoorSound)
                AudioSource.PlayClipAtPoint(unlockedDoorSound, transform.position);
                    
            if (PlayDialogueLines)
                GameManager.Instance.playerHud.UpdateDialogueText(linesWhenUnlocked[Random.Range(0, linesWhenUnlocked.Count)], 2);
        }
        else
        {
            if (PlayDoorSound)
                AudioSource.PlayClipAtPoint(lockedDoorSound, transform.position);

            if (PlayDialogueLines)
                GameManager.Instance.playerHud.UpdateDialogueText(linesWhenLocked[Random.Range(0, linesWhenLocked.Count)], 2);
            else
                Debug.Log("Player is not in front of the door.");
        }
    }

    public int Priority => priority;
}
