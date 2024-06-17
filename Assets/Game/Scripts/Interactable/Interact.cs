// Alvin Philips
// June 11th, 2024
// Player Interact script to handle all interactions.

using TMPro;
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
        [SerializeField] private TextMeshProUGUI interactText; // Reference to the UI Text

        private PlayerInput _playerInput;
        private CharacterBase _player;
        private IInteractable _currentInteractable;
        private Collider[] _interactables = new Collider[10];
        private bool _overrideInteractable;

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _player = GetComponent<CharacterBase>();
            SubscribeToEvents();
            interactText.gameObject.SetActive(false); // Hide the text initially
        }

        private void Update()
        {
            CheckForInteractable();
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

        private void CheckForInteractable()
        {

            IInteractable highestPriorityInteractable = null;

            var count = Physics.OverlapSphereNonAlloc(interactCheckPosition.position, interactCheckRadius, _interactables, interactableLayerMask.value);

            for (var i = 0; i < count; i++)
            {
                if (_interactables[i].TryGetComponent(out IInteractable interactable))
                {
                    highestPriorityInteractable ??= interactable;

                    if (highestPriorityInteractable.Priority < interactable.Priority)
                    {
                        highestPriorityInteractable = interactable;
                    }
                }
            }

            if (highestPriorityInteractable != null)
            {
                // We now have a different Interactable as the highest priority
                if (_currentInteractable != highestPriorityInteractable)
                {
                    if (_currentInteractable != null)
                    {
                        _currentInteractable.Action.performed -= OverrideInteract;
                    }
                    if (highestPriorityInteractable.Action.bindings.Count > 0)
                    {
                        highestPriorityInteractable.Action.Enable();
                        highestPriorityInteractable.Action.performed += OverrideInteract;
                        _overrideInteractable = true;
                    }
                    else
                    {
                        _overrideInteractable = false;
                    }
                }

                _currentInteractable = highestPriorityInteractable;

                string bindingDisplayString;

                if (highestPriorityInteractable.Action.bindings.Count > 0)
                {
                    bindingDisplayString = _currentInteractable.Action.GetBindingDisplayString();
                } else
                {
                    bindingDisplayString = "E";
                }
                interactText.text = bindingDisplayString;
                interactText.gameObject.SetActive(true); // Show the text

            }
            else
            {
                _overrideInteractable = false;
                _currentInteractable = null;
                interactText.gameObject.SetActive(false); // Hide the text
            }
        }

        private void OverrideInteract(InputAction.CallbackContext context)
        {
            _currentInteractable?.Interact(_player);
        }

        public void OnInteract(InputValue value)
        {
            if (_overrideInteractable) return;

            _currentInteractable?.Interact(_player);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.5f, 0, 1.0f);
            Gizmos.DrawWireSphere(interactCheckPosition.position, interactCheckRadius);
        }
    }
}

