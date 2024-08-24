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

            GameManager.Instance.StartLevel(SceneName);
        }
    }

    public int Priority => priority;

    
    
}

