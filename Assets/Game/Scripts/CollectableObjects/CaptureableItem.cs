using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureableItem : MonoBehaviour
{
    public GameObject interactPopup; //Popup to show when Player is Close
    
    public GameObject photoPrefab; //The one that is generated when pic is captured

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.playerRef.photoCapture.capturablePhoto = photoPrefab;
            if (interactPopup != null)
            {
                interactPopup.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.playerRef.photoCapture.capturablePhoto = null;
            if (interactPopup != null)
            {
                interactPopup.SetActive(false);
            }
        }
    }
}
