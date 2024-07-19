// Alvin Philips
// June 11th, 2024
// Player Interact script to handle all interactions.

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game.Scripts.Interactable
{
    [Serializable]
    public struct InteractButton
    {
        public string text;
        public Sprite image;
    }

    [RequireComponent(typeof(CharacterBase))]
    public class Interact : MonoBehaviour
    {
        [SerializeField] private Transform interactCheckPosition;
        [SerializeField, Range(0, 5)] private float interactCheckRadius = 1.0f;
        [SerializeField, Tooltip("What Layers can we interact with")] private LayerMask interactableLayerMask;
        [SerializeField] private Image interactImage;

        [SerializeField] private InteractButton defaultInteraction;
        [SerializeField] private List<InteractButton> interactionButtons;

        private Dictionary<string, InteractButton> _interactionButtonPrompts = new();

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
            interactImage.gameObject.SetActive(false);

            foreach (var button in interactionButtons)
            {
                _interactionButtonPrompts[button.text] = button; 
            }
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
            GameObject interactableObject = gameObject;

            var count = Physics.OverlapSphereNonAlloc(interactCheckPosition.position, interactCheckRadius, _interactables, interactableLayerMask.value);

            for (var i = 0; i < count; i++)
            {
                if (_interactables[i].TryGetComponent(out IInteractable interactable))
                {
                    highestPriorityInteractable ??= interactable;
                    // We do not have an iteractableObject set 
                    if (interactableObject == gameObject)
                    {
                        interactableObject = _interactables[i].gameObject;
                    }

                    if (highestPriorityInteractable.Priority < interactable.Priority)
                    {
                        highestPriorityInteractable = interactable;
                        interactableObject = _interactables[i].gameObject;
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
                if (interactableObject != gameObject)
                {
                    interactImage.transform.position = Camera.main.WorldToScreenPoint(interactableObject.transform.position);
                }
                interactImage.sprite = GetInteractionButtonFromString(bindingDisplayString);
                interactImage.gameObject.SetActive(true); // Show the text

            }
            else
            {
                _overrideInteractable = false;
                _currentInteractable = null;
              //  interactImage.gameObject.SetActive(false); // Show the text
            }
        }

        private Sprite GetInteractionButtonFromString(string key)
        {
            if (_interactionButtonPrompts.TryGetValue(key, out var button))
            {
                return button.image;
            }

            return defaultInteraction.image;
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

