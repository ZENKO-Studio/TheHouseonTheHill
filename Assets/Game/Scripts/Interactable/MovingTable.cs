using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.Interactable;
using UnityEngine.InputSystem;

public class MovingTable : MonoBehaviour
{ 
    private bool bInteracting;
    private NellController playerController;
    private Vector3 offsetVec;

    //Two Snap Points for left and right side
    [SerializeField] Transform snapPoint1;
    [SerializeField] Transform snapPoint2;

    [SerializeField] GameObject btnPrompt;

    [Range(0f, 10f)]
    [SerializeField] private float moveDist = 3f;
    [SerializeField] private float moveSpeed = 1f;

    private Vector3 deltaPos;
    private bool bCanInteract;
    private float deltaP = 0;

    [HideInInspector]
    public InputAction Action => throw new System.NotImplementedException();

    [HideInInspector]
    public int Priority => throw new System.NotImplementedException();

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NellController>(out playerController))
        {
            if(btnPrompt)
                btnPrompt.SetActive(true);

            playerController.PlayerInteracted.AddListener(Interact);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<NellController>(out playerController))
        {
            if (btnPrompt)
                btnPrompt.SetActive(false);

            playerController.PlayerInteracted.RemoveListener(Interact);
        }
    }

    public void Interact()
    {
        //Figure out the closest snap point 
            Transform closestSnapPoint = Vector3.Distance(playerController.transform.position, snapPoint1.position) <= Vector3.Distance(playerController.transform.position, snapPoint2.position) ? snapPoint1 : snapPoint2;

        playerController.Teleport(closestSnapPoint);
    }
}
