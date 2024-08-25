using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTrigger : MonoBehaviour
{
    [SerializeField] GameObject btnPopup;
    void Start()
    {
        btnPopup.SetActive(false);
    }

    private void OnTriggerEnter()
    {
        btnPopup.SetActive(true);
    }
    private void OnTriggerExit() 
    {
        btnPopup.SetActive(false);
    }
}
