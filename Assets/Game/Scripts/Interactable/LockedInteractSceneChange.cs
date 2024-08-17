using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Interactable;
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
    public int SceneNumber = 0;
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
            GameManager.Instance.StartLevel(SceneNumber);
            
        }
    }
    public int Priority => priority;
}

