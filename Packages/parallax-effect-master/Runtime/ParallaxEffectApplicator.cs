using Codice.CM.Client.Differences;
using System.Collections.Generic;
using UnityEngine;

namespace Sxm.ParallaxEffect
{
    public sealed class ParallaxEffectApplicator : MonoBehaviour
    {
        public const string MotionSourceName = nameof(motionSource);
        public const string TargetCameraName = nameof(targetCamera);
        public const string SelfSpeedName = nameof(selfSpeed);
        public const string SelfDirectionName = nameof(selfDirection);
        public const string ForceRootLookAtCameraName = nameof(forceRootLookAtCamera);
        public const string LayersRootName = nameof(layersRoot);
        public const string ParallaxLayersName = nameof(parallaxLayers);

        public MotionSource SelectedMotionSource => motionSource;

        [SerializeField] private MotionSource motionSource = MotionSource.Camera;
        [SerializeField] private Camera targetCamera;
        [SerializeField, Range(0f, 1f)] private float selfSpeed = 1f;
        [SerializeField] private MovementDirection selfDirection = MovementDirection.Left;

        [Space]
        [SerializeField] private bool forceRootLookAtCamera = true;
        [SerializeField] private RectTransform layersRoot = null;

        [Space]
        [SerializeField] private ParallaxLayer[] parallaxLayers = null;

        private float _initialCameraVerticalPosition = 0f;
        private float _lastCameraHorizontalPosition = 0f;
        private Quaternion _lastCameraRotation = Quaternion.identity;

        private static readonly Dictionary<MovementDirection, Vector3> MovementDirectionMap = new Dictionary<MovementDirection, Vector3>
        {
            { MovementDirection.Left, Vector3.left },
            { MovementDirection.Right, Vector3.right }
        };

        private void Awake()
        {
            if (targetCamera == null)
            {
                targetCamera = Camera.main;
            }
        }

        private void Start()
        {
            Transform cameraTransform = targetCamera.transform;

            _initialCameraVerticalPosition = cameraTransform.position.y;
            _lastCameraHorizontalPosition = cameraTransform.position.x;
            _lastCameraRotation = cameraTransform.rotation;

            AddSideLayers();
        }

        private void Update()
        {
            if (forceRootLookAtCamera)
            {
                ForceLayersRootLookAtCamera();
            }

            ApplyParallax();
            WrapLayers();
        }

        private void ForceLayersRootLookAtCamera()
        {
            Transform cameraTransform = targetCamera.transform;

            if (cameraTransform.rotation != _lastCameraRotation)
            {
                layersRoot.position = cameraTransform.position + cameraTransform.forward + cameraTransform.TransformDirection(-_initialCameraVerticalPosition * Vector3.up);
                layersRoot.rotation = Quaternion.LookRotation(cameraTransform.forward, cameraTransform.up);

                _lastCameraRotation = cameraTransform.rotation;
            }
        }

        private void ApplyParallax()
        {
            float currentCameraPosition = targetCamera.transform.position.x;

            Vector3 deltaDistance = motionSource == MotionSource.Camera
                ? Vector3.right * (currentCameraPosition - _lastCameraHorizontalPosition)
                : selfSpeed * Time.deltaTime * MovementDirectionMap[selfDirection];
            _lastCameraHorizontalPosition = currentCameraPosition;

            foreach (var layer in parallaxLayers)
            {
                layer.TranslateWithParallax(deltaDistance, motionSource);
            }
        }

        private void WrapLayers()
        {
            foreach (var layer in parallaxLayers)
            {
                float signDistance = layer.RectTransform.InverseTransformPoint(targetCamera.transform.position).x;

                Vector3 direction = Vector3.right * Mathf.Sign(signDistance);
                float distance = Mathf.Abs(signDistance);
                float layerRelativeWidth = layer.RectTransform.rect.width;

                if (distance > layerRelativeWidth)
                {
                    layer.Translate(direction * layerRelativeWidth * parallaxLayers.Length);
                }
            }
        }

        private void AddSideLayers()
        {
            foreach (var layer in parallaxLayers)
            {
                RectTransform parent = layer.RectTransform;
                string parentName = parent.gameObject.name;

                Vector3 horizontalOffset = Vector3.right * parent.rect.width;
                var leftSideLayer = Instantiate(parent, parent.position - horizontalOffset, Quaternion.identity);
                var rightSideLayer = Instantiate(parent, parent.position + horizontalOffset, Quaternion.identity);

                leftSideLayer.gameObject.name = $"{parentName} - Left";
                rightSideLayer.gameObject.name = $"{parentName} - Right";

                leftSideLayer.SetParent(parent.parent);
                rightSideLayer.SetParent(parent.parent);

                leftSideLayer.sizeDelta = rightSideLayer.sizeDelta = Vector2.zero;
            }
        }

        // Method to toggle direction
        public void ToggleDirection()
        {
            selfDirection = selfDirection == MovementDirection.Left ? MovementDirection.Right : MovementDirection.Left;
        }

        public enum MotionSource
        {
            Self,
            Camera
        }
    }
}
