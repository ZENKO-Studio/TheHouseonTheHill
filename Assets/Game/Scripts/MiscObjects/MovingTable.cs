using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTable : MonoBehaviour
{
    bool bInteracting;

    ThirdPersonController playerController;

    private Vector3 offsetVec;

    [Range(0f, 10f)]
    [SerializeField] float moveDist = 3f;
    [SerializeField] float moveSpeed = 1f;

    private Vector3 deltaPos;
    private bool bCanInteract;

    private void Start()
    {
        bInteracting = false;
        offsetVec = new Vector3(0, 0, -1.5f);
        deltaPos = transform.position + new Vector3(0, 0, moveDist);
    }

    float deltaP = 0;

    // Update is called once per frame
    void Update()
    {
        if (!bCanInteract)
            return;

        if(Input.GetKeyDown(KeyCode.E)) 
        {
            bInteracting = !bInteracting;
            if (playerController)
                playerController.CanMove(!bInteracting);
        }

        if (bInteracting && playerController != null)
        {
            if (deltaP < moveDist)
            {
                float playerInput = Mathf.Clamp01(playerController._input.move.y);
                transform.Translate(transform.forward * playerInput * moveSpeed * Time.deltaTime);
                deltaP += playerInput * moveSpeed * Time.deltaTime;
            }

            playerController.transform.position = transform.position + offsetVec;
            playerController.transform.rotation = transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bCanInteract = true;
        other.TryGetComponent<ThirdPersonController>(out playerController);
    }

    private void OnTriggerExit(Collider other)
    {
        bCanInteract = false;
    }
}
