using Game.Scripts.Interactable;
using System.Collections;
using System.Collections.Generic;
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

        public InputAction Action => interactAction;
        public void Interact(CharacterBase player)
        {
          
            if (InventoryHandler.Instance.HasKey(keyToUnlockInteger))
            {
                Debug.Log("FS");
                isLocked = false;
                onInteract?.Invoke();
            }
        }
        public int Priority => priority;
    }
}