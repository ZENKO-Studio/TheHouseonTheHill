// Alvin Philips
// June 11th, 2024
// Default interactable implementation. Place on *anything* to react to interactions.

using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Interactable
{
    public class DummyInteractable : MonoBehaviour, IInteractable
    {
        public UnityEvent onInteract;
        [SerializeField] private int priority;
        public void Interact(CharacterBase player)
        {
            onInteract?.Invoke();
        }
        public int Priority => priority;
    }
}