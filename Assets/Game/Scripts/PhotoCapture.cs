using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoCapture : MonoBehaviour
{
    //To take from the associated wall
    [HideInInspector] public GameObject capturablePhoto;

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void CapturePhoto()
    {
        if (capturablePhoto == null)
            return;

        StartCoroutine(CameraFlashEffect());
        capturablePhoto.GetComponent<InventoryItem>().Interact();
        //Show the Photo (idk how)
        capturablePhoto = null;
    }

    IEnumerator CameraFlashEffect()
    {
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }

    
}
