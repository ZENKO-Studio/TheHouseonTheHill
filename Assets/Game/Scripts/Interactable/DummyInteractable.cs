// Alvin Philips
// June 11th, 2024
// Default interactable implementation. Place on *anything* to react to interactions.
//revised by Brandon Bennie
//08/24/24 

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.Scripts.Interactable
{
    public class DummyInteractable : MonoBehaviour, IInteractable
    {
            
        [Header("Audio and Dialogue")]
        [SerializeField] bool PlayDoorSound = false;

        [SerializeField] private AudioClip dummyInteract;
        
        public UnityEvent onInteract;
        public UnityEvent AfterInteract;
        [SerializeField] private InputAction interactAction;
        [SerializeField] private int priority;

        public InputAction Action => interactAction;
        public void Interact(CharacterBase player)
        {
            onInteract?.Invoke();
          
            if (PlayDoorSound)
                AudioSource.PlayClipAtPoint(dummyInteract, transform.position);
            else
                Debug.Log("Player is not in front of the door.");
            
            AfterInteract?.Invoke();
        }
        
        public int Priority => priority;
    }
}