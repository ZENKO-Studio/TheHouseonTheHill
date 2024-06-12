using System.Collections;
// Alvin Philips
// June 11th, 2024
// Interactable interface that interactable objects can implement.

namespace Game.Scripts.Interactable
{
    public interface IInteractable
    {
        public void Interact(CharacterBase player);
    }
}