using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LockedInteractSceneChange : MonoBehaviour
{
    [SerializeField] private int keyToUnlockInteger = 0;
    [SerializeField] private bool isLocked = true;

    public UnityEvent onInteract;
    public UnityEvent onUnlock;
    public string SceneName;
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
            SceneManager.LoadScene(SceneName);
            
        }
    }
    public int Priority => priority;
}

