using Game.Scripts.Interactable;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.Scripts.Interactable
{
    public class LockedInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private int keyToUnlockInteger = 0;
        [SerializeField] private bool isLocked = true;

        public UnityEvent onInteract;
        public UnityEvent onUnlock;
        [SerializeField] private InputAction interactAction;
        [SerializeField] private int priority;

        #region Sami added Dialogue Lines and Audio Clips for the door
        [Header("Audio and Dialogue")]
        [SerializeField] bool bPlayDoorSound = false;
        [SerializeField] bool bPlayDialogueLines = false;

        [SerializeField] AudioClip lockedDoorSound; 

        [SerializeField] AudioClip unlockedDoorSound; 

        [Tooltip("Dialogue that will be played once when player enters the trigger and do not have required items")]
        [SerializeField] List<string> linesWhenLocked = new List<string>();

        [Tooltip("Dialogue that will be played once when player enters the trigger have all the items")]
        [SerializeField] List<string> linesWhenUnlocked = new List<string>();
        #endregion

        public InputAction Action => interactAction;
        public void Interact(CharacterBase player)
        {
          
            if (InventoryHandler.Instance.HasKey(keyToUnlockInteger))
            {
                Debug.Log("FS");
                isLocked = false;
                onInteract?.Invoke();

                InventoryItem keyItem = InventoryHandler.Instance.keys
                    .FirstOrDefault(k => k.Key.itemId == keyToUnlockInteger).Key;
                if (keyItem != null)
                {
                    InventoryHandler.Instance.RemoveItem(keyItem);
                }

                #region Sami`s Addition! One if bracket is in here so dont worry!
                if (bPlayDoorSound)
                    AudioSource.PlayClipAtPoint(unlockedDoorSound, transform.position);
                    
                if (bPlayDialogueLines)
                    GameManager.Instance.playerHud.UpdateDialogueText(linesWhenUnlocked[Random.Range(0, linesWhenUnlocked.Count)], 2);
            }
            else
            {
                if (bPlayDoorSound)
                    AudioSource.PlayClipAtPoint(lockedDoorSound, transform.position);

                if (bPlayDialogueLines)
                    GameManager.Instance.playerHud.UpdateDialogueText(linesWhenLocked[Random.Range(0, linesWhenLocked.Count)], 2);
            }
            #endregion

        }
        public int Priority => priority;
    }
}