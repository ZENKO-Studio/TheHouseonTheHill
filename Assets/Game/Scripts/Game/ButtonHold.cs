using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class ButtonHold : MonoBehaviour
{    [SerializeField] private InputAction holdAction;
    public int SceneNumber = 1;
    public float holdDuration = 2f;
    private float holdTimer = 0f;
    private bool isHolding = false;
    public PlayableDirector timeLine;

    public InputAction Action => holdAction;

    void OnEnable()
    {
        holdAction.Enable();
        holdAction.performed += OnButtonHold;
        holdAction.canceled += OnButtonRelease;
    }

    void OnDisable()
    {
        holdAction.Disable();
        holdAction.performed -= OnButtonHold;
        holdAction.canceled -= OnButtonRelease;
    }

    private void OnButtonHold(InputAction.CallbackContext context)
    {
        // Start counting the hold time only if the button is held down
        if (holdAction.ReadValue<float>() > 0 && !isHolding)
        {
            StartCoroutine(HoldButton());
        }
    }

    private void OnButtonRelease(InputAction.CallbackContext context)
    {
        // Reset timer and holding state when button is released
        StopCoroutine(HoldButton());
        holdTimer = 0f;
        isHolding = false;
    }

    private IEnumerator HoldButton()
    {
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
    }

    void OnHoldComplete()
    {
      
        
        GameManager.Instance.StartLevel(SceneNumber);
        timeLine.Stop();
        
    }

  
}
