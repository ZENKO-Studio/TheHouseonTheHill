// Alvin Philips
// June 11th, 2024
// Default interactable implementation. Place on *anything* to react to interactions.

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.Scripts.Interactable
{
    public class DummyInteractable : MonoBehaviour, IInteractable
    {
        public UnityEvent onInteract;
        [SerializeField] private InputAction interactAction;
        [SerializeField] private int priority;

        public InputAction Action => interactAction;
        public void Interact(CharacterBase player)
        {
            onInteract?.Invoke();
        }
        public int Priority => priority;
    }
}