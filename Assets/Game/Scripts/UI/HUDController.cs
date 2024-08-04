using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Health And Stamina")]
    [SerializeField] Slider healthBar;
    [SerializeField] Slider staminaBar;

<<<<<<< HEAD
=======
    [Header("Salt Charges")]
    [SerializeField] TMP_Text saltText;

>>>>>>> Developing
    [Header("Flashlight")]
    [SerializeField] Image flashlightImage;

    [SerializeField] Sprite flashOnSprite;
    [SerializeField] Sprite flashOffSprite;

    [SerializeField] HUDMenu hudMenu;

    NellController nellController;
    Flashlight flashlight;

    [Header("Blink Thing")]
    [SerializeField] Animator blinkAnimator;

    [Header("Dialogue")]
    [SerializeField] Transform dialoguePanel;
    [SerializeField] TMP_Text dialogueText;

    // Start is called before the first frame update
    void OnEnable()
    {
<<<<<<< HEAD
        nellController = GameManager.Instance.playerRef;

        if (nellController != null)
        {
            nellController.OnHealthChanged.AddListener(UpdateHealthbar);
            nellController.OnStaminaChanged.AddListener(UpdateStaminabar);
            flashlight = nellController.flashlight;

            if (flashlight != null)
                flashlight.OnFlashLightToggle.AddListener(UpdateFlashlightIcon);

            Debug.Log("Listener Added!");
        }
        else
        {
            Debug.Log("Listener Not Added!");

        }

=======
        GameManager.Instance.OnPlayerSpawned.AddListener(HandlePlayerSpawn);
>>>>>>> Developing

        //hudMenu.Invoke("HideHUD", 5f);
    }

<<<<<<< HEAD
    

    //// Update is called once per frame
    //void Update()
    //{

    //}
=======
    private void HandlePlayerSpawn()
    {
        nellController = GameManager.Instance.playerRef;
        GameManager.Instance.playerHud = this;

        if (nellController != null)
        {
            nellController.OnHealthChanged.AddListener(UpdateHealthbar);
            nellController.OnStaminaChanged.AddListener(UpdateStaminabar);
            nellController.saltChargeHandler.OnSaltChanged.AddListener(UpdateSaltCount);
            flashlight = nellController.flashlight;

            if (flashlight != null)
                flashlight.OnFlashLightToggle.AddListener(UpdateFlashlightIcon);

            UpdateSaltCount();

            Debug.Log("Listener Added!");
        }
        else
        {
            Debug.Log("Listener Not Added!");

        }
    }
>>>>>>> Developing

    void UpdateHealthbar()
    {
        Debug.Log("UpdatingHealthBar");
        healthBar.value = GameManager.Instance.playerRef.GetHealth();
        //hudMenu.ShowHUD();
        //hudMenu.Invoke("HideHUD", 5f);
    }
    
    void UpdateStaminabar()
    {
        Debug.Log("UpdatingStaminaBar");
        staminaBar.value = GameManager.Instance.playerRef.GetStamina();
        //hudMenu.ShowHUD();
        //hudMenu.Invoke("HideHUD", 5f);
    }

<<<<<<< HEAD
=======
    private void UpdateSaltCount()
    {
        if(saltText)
        {
            saltText.text = nellController.saltChargeHandler.CurrentSaltCharges.ToString();
        }
    }

>>>>>>> Developing
    private void UpdateFlashlightIcon()
    {
        flashlightImage.sprite = flashlight.IsOn() ? flashOnSprite : flashOffSprite;
    }

    void OnDisable()
    {
        if (nellController != null)
        {
            nellController.OnHealthChanged.RemoveListener(UpdateHealthbar);
            nellController.OnStaminaChanged.RemoveListener(UpdateStaminabar);
<<<<<<< HEAD
=======
            nellController.saltChargeHandler.OnSaltChanged.RemoveListener(UpdateSaltCount);

>>>>>>> Developing
        }

        if (flashlight != null)
            flashlight.OnFlashLightToggle.RemoveListener(UpdateFlashlightIcon);
<<<<<<< HEAD
=======

        GameManager.Instance.OnPlayerSpawned.RemoveListener(HandlePlayerSpawn);
>>>>>>> Developing
    }

    public void UpdateDialogueText(string text, int disableTime = 5)
    {
        CancelInvoke(nameof(DisableDialoguePanel));

        dialoguePanel.gameObject.SetActive(true);
        dialogueText.text = text;

        Invoke(nameof(DisableDialoguePanel), disableTime);
        
    }

    void DisableDialoguePanel()
    {
        dialogueText.text = String.Empty;
        dialoguePanel.gameObject.SetActive(false);
    }

    CinemaMachineSwitcher cinemaMachineSwitcher = null;

    internal void PlayBlinkAnim(CinemaMachineSwitcher s)
    {
        Debug.Log("PlayingBlinkAnimCalled");
        if (blinkAnimator)
        {
            Debug.Log("PlayingBlinkAnimCalled and BlinkAnimator not null");
            blinkAnimator.SetTrigger("Blink");
            cinemaMachineSwitcher = s;
        }
    }

    public void OnBlink(AnimationEvent animationEvent)
    {
        Debug.Log("OnBlinkCalled");

        if (cinemaMachineSwitcher != null)
            cinemaMachineSwitcher.Blink();

        cinemaMachineSwitcher = null;
    }
}
