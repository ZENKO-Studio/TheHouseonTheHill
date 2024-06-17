using System.Collections;
using UnityEngine.InputSystem;
// Alvin Philips
// June 11th, 2024
// Interactable interface that interactable objects can implement.

namespace Game.Scripts.Interactable
{
    public interface IInteractable
    {
        public void Interact(CharacterBase player);
        public InputAction Action { get; }
        // public KeyCode InteractKey = KeyCode.E;
        int Priority { get; }
    }
}