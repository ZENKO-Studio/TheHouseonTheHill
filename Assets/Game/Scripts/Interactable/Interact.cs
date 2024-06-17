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
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            if (_playerInput != null)
            {
                //_playerInput.actions["Interact"].performed += OnInteract;
            }
        }

        private void UnsubscribeFromEvents()
        {
            if (_playerInput != null)
            {
                //_playerInput.actions["Interact"].performed -= OnInteract;
            }
        }

        public void OnInteract(InputValue value)
        {
            var colliders = Physics.OverlapSphere(interactCheckPosition.position, interactCheckRadius, interactableLayerMask.value);
            IInteractable highestPriorityInteractable = null;
            int highestPriority = int.MinValue;

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IInteractable interactable))
                {
                    if (interactable.Priority > highestPriority)
                    {
                        highestPriority = interactable.Priority;
                        highestPriorityInteractable = interactable;
                    }
                }
            }

            highestPriorityInteractable?.Interact(_player);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.5f, 0, 1.0f);
            Gizmos.DrawWireSphere(interactCheckPosition.position, interactCheckRadius);
        }
    }
}

