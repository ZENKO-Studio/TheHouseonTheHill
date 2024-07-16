using System;
using UnityEngine;

public class MovableObject : MonoBehaviour
{ 
    
    private NellController playerController;

    //Two Snap Points for left and right side
    [SerializeField] Transform snapPoint1;
    [SerializeField] Transform snapPoint2;

    [SerializeField] GameObject btnPrompt;

    bool bWalkingTowards = false;
    bool bMovingObject = false; 
    
    [Range(0f, 10f)]
    public float moveDist = 3f;
    
    internal float deltaP = 0;

    internal Transform closestSnapPoint;

    private Vector2 startPos;

    private void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NellController>(out playerController))
        {
            if(btnPrompt)
                btnPrompt.SetActive(true);

            playerController.PlayerInteracted.AddListener(Interact);
        }
    }

    private void Update()
    {
        if(bWalkingTowards)
        {
            MoveTowardsObject();
        }
        else if(bMovingObject)
        {
            MoveObject();
        }
    }

    private void LateUpdate()
    {
        //bMovingObject = true;AnimLerp();
    }

    private void MoveTowardsObject()
    {
        if(!bMovingObject)
        {
            //Calculate Target Direction
            Vector3 targetDir;
            targetDir = new Vector3(closestSnapPoint.position.x - playerController.transform.position.x,
                0f,
                closestSnapPoint.position.z - playerController.transform.position.z);

            Quaternion rot = Quaternion.LookRotation(targetDir);
            playerController.transform.rotation = Quaternion.Slerp(playerController.transform.rotation, rot, .05f);

            playerController.nellsAnimator.SetBool("IsMoving", true);
            playerController.nellsAnimator.SetFloat("InputMagnitude", 1f, 0.05f, Time.deltaTime);


            if (Vector3.Distance(playerController.transform.position, closestSnapPoint.transform.position) < 0.1f)
            {
                playerController.nellsAnimator.SetBool("IsMoving", false);
                playerController.nellsAnimator.SetFloat("InputMagnitude", 0f, 0.05f, Time.deltaTime);
                playerController.transform.position = closestSnapPoint.transform.position;
                playerController.transform.rotation = closestSnapPoint.transform.rotation;
                bWalkingTowards = false;
                AttachObject();
            }
        }
    }

    private void AnimLerp()
    {
        if (!bMovingObject) return;

        if (Vector3.Distance(playerController.transform.position, closestSnapPoint.transform.position) > 0.1f)
        {
            playerController.transform.position = Vector3.Lerp(playerController.transform.position, closestSnapPoint.position, Time.deltaTime * .5f);
            playerController.transform.rotation = Quaternion.Slerp( playerController.transform.rotation, closestSnapPoint.transform.rotation, Time.deltaTime * .5f);
        }
        else
        {
            playerController.transform.position = closestSnapPoint.transform.position;
            playerController.transform.rotation = closestSnapPoint.transform.rotation;
            bWalkingTowards = false;
        }
    }

    //After the 
    private void AttachObject()
    {
        playerController.nellsAnimator.SetBool("PushObject", true);
        transform.parent = playerController.transform;
        bMovingObject = true;
    }

    private void MoveObject()
    {
        playerController.nellsAnimator.SetFloat("InputMagnitude", playerController.moveInput.y, 0.05f, Time.deltaTime);
        if(Vector2.Distance(startPos, new Vector2(transform.position.x, transform.position.z)) > moveDist)
        {
            RemoveObject();
        }
    }

    private void RemoveObject()
    {
        //Disconect Player
        playerController.nellsAnimator.SetBool("PushObject", false);
        playerController.SetPlayerHasControl(true);
        transform.parent = null;
        bWalkingTowards = false;
        bMovingObject = false;
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
        if(bWalkingTowards ||  bMovingObject)
        {
            RemoveObject();
        }
        else
        {
            //Figure out the closest snap point 
            closestSnapPoint = Vector3.Distance(playerController.transform.position, snapPoint1.position) <= Vector3.Distance(playerController.transform.position, snapPoint2.position) ? snapPoint1 : snapPoint2;
            playerController.SetPlayerHasControl(false);
            bWalkingTowards = true;
        }
        
    }
}
