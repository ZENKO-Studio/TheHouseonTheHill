using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplay;
    [SerializeField] private GameObject PhotoFrame;
    [SerializeField] private GameObject photoPrefab; // Prefab for the photo game object
    [SerializeField] private Transform normalPhotoContainer; // Container to store normal photos
    [SerializeField] private Transform keyItemContainer; // Container to store key item photos

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fadingAnimation;

    [Header("Player Disable")]
    [SerializeField] private GameObject player;
    [SerializeField] private List<Object> objectsToCheck; // List of ObjectItem instances to check if they are in view

    [Header("Capture Mode UI")]
    [SerializeField] private Canvas captureModeCanvas;

    [SerializeField] private Camera mainCamera;

    private bool viewingPhoto;
    private bool isPlayerActive = true;
    private bool isCaptureMode = false;

    private void Start()
    {
        mainCamera = Camera.main;
        if (captureModeCanvas != null)
        {
            captureModeCanvas.enabled = false; // Ensure the canvas is initially disabled
        }
    }

    private void Update()
    {
        if (!isCaptureMode)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!viewingPhoto)
            {
                // Check if any object in the list is in view
                bool isKeyItem = false;
                foreach (var obj in objectsToCheck)
                {
                    if (CameraUtilities.IsObjectInViewAndWithinArea(mainCamera, obj.gameObject))
                    {
                        isKeyItem = obj is KeyItem;
                        break;
                    }
                }

                StartCoroutine(CapturePhoto(isKeyItem)); // Capture photo based on whether a key item is in view
            }
            else
            {
                RemovePhoto();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            TogglePlayer();
        }
    }

    public void ToggleCapture()
    {
        isCaptureMode = !isCaptureMode;
        player.SetActive(!isCaptureMode); // Disable player control when in capture mode

        if (captureModeCanvas != null)
        {
            captureModeCanvas.enabled = isCaptureMode; // Enable or disable the capture mode canvas
        }
    }

    void TogglePlayer()
    {
        isPlayerActive = !isPlayerActive;
        player.SetActive(isPlayerActive);
    }

    IEnumerator CapturePhoto(bool isKeyItem)
    {
        viewingPhoto = true;

        // Disable the capture mode canvas before taking the screenshot
        captureModeCanvas.enabled = false;

        yield return new WaitForEndOfFrame();

        Texture2D screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        StartCoroutine(CameraFlashEffect());

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();

        // Re-enable the capture mode canvas after taking the screenshot
        captureModeCanvas.enabled = true;

        ShowPhoto(screenCapture, isKeyItem);
    }

    IEnumerator CameraFlashEffect()
    {
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }

    void ShowPhoto(Texture2D screenCapture, bool isKeyItem)
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(.5f, .5f), 100.0f);

        photoDisplay.sprite = photoSprite;

        PhotoFrame.SetActive(true);
        fadingAnimation.Play("PhotoFade");

        if (isKeyItem)
        {
            SaveKeyItemAsGameObject(photoSprite);
        }
        else
        {
            SavePhotoAsGameObject(photoSprite);
        }
    }

    void SavePhotoAsGameObject(Sprite photoSprite)
    {
        GameObject newPhoto = Instantiate(photoPrefab, normalPhotoContainer); // Instantiate new photo in the normal photo container
        newPhoto.GetComponent<Image>().sprite = photoSprite;
        // Optionally set position and other properties of the new photo game object here
    }

    void SaveKeyItemAsGameObject(Sprite photoSprite)
    {
        GameObject newPhoto = Instantiate(photoPrefab, keyItemContainer); // Instantiate new key item photo in the key item container
        newPhoto.GetComponent<Image>().sprite = photoSprite;
        // Optionally set position and other properties of the new key item game object here
    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        PhotoFrame.SetActive(false);
    }
}

