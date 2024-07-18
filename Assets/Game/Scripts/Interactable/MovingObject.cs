using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private bool bInteracting;
    private NellController playerController;
    private Vector3 offsetVec;

    [Range(0f, 10f)]
    [SerializeField] private float moveDist = 3f;
    [SerializeField] private float moveSpeed = 1f;

    private Vector3 deltaPos;
    private bool bCanInteract;
    private float deltaP = 0;

    private void Start()
    {
        bInteracting = false;
        offsetVec = new Vector3(0, 0, -1.5f);
        deltaPos = transform.position + new Vector3(0, 0, moveDist);
    }

    private void Update()
    {
        if (bCanInteract && Input.GetKeyDown(KeyCode.E))
        {
            bInteracting = !bInteracting;
        }

        if (bInteracting && deltaP < moveDist)
        {
            float playerInput = Mathf.Clamp01(playerController.moveInput.y);
<<<<<<<< HEAD:Assets/Game/Scripts/Interactable/MovingTable.cs
            transform.Translate(transform.forward * playerInput * moveSpeed * Time.deltaTime);
========
            transform.Translate(transform.forward * (playerInput * moveSpeed * Time.deltaTime));
>>>>>>>> Developing:Assets/Game/Scripts/Interactable/MovingObject.cs
            deltaP += playerInput * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NellController>(out playerController))
        {
            bCanInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<NellController>(out playerController))
        {
            bCanInteract = false;
            bInteracting = false;
        }
    }
}
