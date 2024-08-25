using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Video;

public class ButtonHold : MonoBehaviour
{   [SerializeField] private InputAction holdAction;
    public int SceneNumber = 1;
    public float holdDuration = 2f;
    private float holdTimer = 0f;
    private bool isHolding = false;
    public VideoPlayer videoPlayer;
    public Canvas canvas;  // Reference to the Canvas that should be disabled

    private Coroutine holdCoroutine;

    public InputAction Action => holdAction;

    void OnEnable()
    {
        holdAction.Enable();
        holdAction.performed += OnButtonHold;  // Subscribe to when the button is pressed
        holdAction.canceled += OnButtonRelease; // Subscribe to when the button is released
    }

    void OnDisable()
    {
        holdAction.Disable();
        holdAction.performed -= OnButtonHold;
        holdAction.canceled -= OnButtonRelease;
    }

    private void OnButtonHold(InputAction.CallbackContext context)
    {
        if (holdCoroutine == null)  // Start the hold process only if it hasn't been started
        {
            holdCoroutine = StartCoroutine(HoldButton());
        }
    }

    private void OnButtonRelease(InputAction.CallbackContext context)
    {
        if (holdCoroutine != null)
        {
            StopCoroutine(holdCoroutine);
            holdCoroutine = null;
        }

        holdTimer = 0f;  // Reset the timer
        isHolding = false;
    }

    private IEnumerator HoldButton()
    {
        holdTimer = 0f;

        while (holdTimer < holdDuration)
        {
            holdTimer += Time.deltaTime;
            yield return null;
        }

        if (holdTimer >= holdDuration)
        {
            isHolding = true;
            OnHoldComplete();
        }

        holdCoroutine = null; // Reset the coroutine reference after completion
    }

    private void OnHoldComplete()
    {
        // Disable the canvas before starting the level
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);  // Disable the canvas
        }
        
        // Whatever logic you want to trigger after holding for the duration
        GameManager.Instance.StartLevel(SceneNumber);
        
        if (videoPlayer != null)
        {
            videoPlayer.Stop();  // Stops the video if a VideoPlayer is attached
        }
    }
  
}
