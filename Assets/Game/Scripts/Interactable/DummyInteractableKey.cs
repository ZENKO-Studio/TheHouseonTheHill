using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DummyInteractableKey : MonoBehaviour
{
    [SerializeField] private string requiredKey;
    private bool _isUnlocked;

    public UnityEvent onUnlock;

    public void Unlock(string key)
    {
        if (key == requiredKey)
        {
            _isUnlocked = true;
            onUnlock?.Invoke();
        }
    }

    public  void Interact(CharacterBase player)
    {
        if (_isUnlocked)
        {
            Interact(player);
        }
        else
        {
            Debug.Log("The object is locked. You need the correct key to unlock it.");
        }
    }
}

