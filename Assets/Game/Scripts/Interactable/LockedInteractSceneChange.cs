using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Interactable;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LockedInteractSceneChange : MonoBehaviour, IInteractable
{
    [SerializeField] private int keyToUnlockInteger = 0;
    [SerializeField] private bool isLocked = true;

    public UnityEvent onInteract;
    public UnityEvent onUnlock;
    public int SceneName = 0;
    [SerializeField] private InputAction interactAction;
    [SerializeField] private int priority;

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
    public InputAction Action => interactAction;

    public void Interact(CharacterBase player)
    {
        if (InventoryHandler.Instance.HasKey(keyToUnlockInteger))
        {
            Debug.Log("FS");
            isLocked = false;
            onInteract?.Invoke();
        
            // Find the key in the inventory and remove it after use
            InventoryItem keyItem = InventoryHandler.Instance.keys
                .FirstOrDefault(k => k.Key.itemId == keyToUnlockInteger).Key;
            if (keyItem != null)
            {
                InventoryHandler.Instance.RemoveItem(keyItem);
            }

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
        }
        GameManager.Instance.StartLevel(SceneName);
        
    }

    public int Priority => priority;

    
    
}

