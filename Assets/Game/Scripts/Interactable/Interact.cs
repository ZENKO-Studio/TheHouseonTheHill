// Alvin Philips
// June 11th, 2024
// Player Interact script to handle all interactions.

using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Interactable
{
    [RequireComponent(typeof(CharacterBase))]
    public class Interact : MonoBehaviour
    {
        [SerializeField] private Transform interactCheckPosition;
        [SerializeField, Range(0, 5)] private float interactCheckRadius = 1.0f;
        [SerializeField, Tooltip("What Layers can we interact with")] private LayerMask interactableLayerMask;

        private PlayerInput _playerInput;
        private CharacterBase _player;

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _player = GetComponent<CharacterBase>();
            // Subscribe to input events or other events if needed
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            // Unsubscribe from events to avoid memory leaks
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            // Example: Subscribe to a key press to interact
            if (_playerInput != null)
            {
                //_playerInput.actions["Interact"].performed += OnInteract;
            }
        }

        private void UnsubscribeFromEvents()
        {
            // Example: Unsubscribe from a key press to interact
            if (_playerInput != null)
            {
                //_playerInput.actions["Interact"].performed -= OnInteract;
            }
        }

        public void OnInteract(InputValue value)
        {
            // Check for interactable objects in front of the player
            // Might have to use 'QueryTriggerInteraction.Ignore' as the last parameter if we want to ignore Colliders set to 'Trigger'
            var colliders = Physics.OverlapSphere(interactCheckPosition.position, interactCheckRadius, interactableLayerMask.value);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(_player);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.5f, 0, 1.0f);
            Gizmos.DrawWireSphere(interactCheckPosition.position, interactCheckRadius);
        }
    }
}
