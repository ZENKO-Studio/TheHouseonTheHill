using UnityEngine;

public class MovableObject : MonoBehaviour
{ 
    
    private NellController playerController;

    //Two Snap Points for left and right side
    [SerializeField] Transform snapPoint1;
    [SerializeField] Transform snapPoint2;

    [SerializeField] GameObject btnPrompt;

    [Range(0f, 10f)]
    public float moveDist = 3f;
    
    internal float deltaP = 0;

    internal Transform closestSnapPoint;

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
        closestSnapPoint = Vector3.Distance(playerController.transform.position, snapPoint1.position) <= Vector3.Distance(playerController.transform.position, snapPoint2.position) ? snapPoint1 : snapPoint2;

        if (playerController.movingObj == null)
            playerController.movingObj = this;
        else
            playerController.movingObj = null;
    }
}
