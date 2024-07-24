/** @SAMI 06-06-24
 *  This script handles movement and other stuff related to Nell (Player Controller particularly)
 **/
using Cinemachine;
using Game.Scripts.Interactable;
using GameCreator.Runtime.Common.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
using static EventBus;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SaltChargeHandler))]
public class NellController : CharacterBase
{
    public CharacterController characterController;
    public Animator nellsAnimator;
    
    #region Brandon region
    
    public Transform holdPosition; // Position where the item will be held
    private GameObject heldItem;
    
    #endregion
    
    
    #region Character Control Values
    [Header("Character Controls")]
    
    public bool bEnableMovement = true;
    [SerializeField] float rotSpeed = 300f;

    //Can be used if jumping required
    [SerializeField] float jumpSpeed = 4f;

    [Tooltip("How much the character should move forward when moving and jumping")]
    [SerializeField] float forwardJumpForce = 3f;

    [Tooltip("How much the character should move forward when swimming")]
    [SerializeField] float swimSpeed = 2f;

    float ogStepOffset;
    float ySpeed = 0f;

    [Tooltip("Height of Character Collider when crouched (can be tweaked with animation if needed)")]
    [SerializeField] float crouchHeight = 1.28f;
    private float crouchCenter;
    private float defaultHeight;
    private float defaultCenter;

    //Some variables for Animation Control
    internal bool bMoving;
    private bool bJumping;
    private bool bGrounded;
    private bool bFalling;

    //This is the variable that can be changed to take control away from player and give back to player
    bool bPlayerHasControl = true;

    //For Under water region
    internal bool bSwimming;


    #endregion

    #region Sound And Audio

    [Header("How far should the sound be heard")]
    [SerializeField] float crouchSound = 0f;
    [SerializeField] float walkSound = 5f;
    [SerializeField] float runSound = 8f;

    float soundRange = 0f;

    public AudioClip[] FootstepAudioClips;
    [Range(0, 1)] public float FootstepAudioVolume = 1f;

    #endregion

    #region Camera Stuff
    [Header("Cinemachine")]

    [SerializeField] private CinemachineVirtualCamera cineCam;

    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject camTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    public float camYUp = 5.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float camYDown = -15.0f;

    [Tooltip("Additional degrees to override the camera. Useful for fine-tuning camera position when locked")]
    [HideInInspector] public float CameraAngleOverride = 0.0f;

    [Tooltip("For locking the camera position on all axes")]
    [HideInInspector] public bool LockCameraPosition = false;

    [Tooltip("How Close the camera should be when zoomed in, default distance is 3")]
    [SerializeField] float camZoomInDistance = 1f;

    [Tooltip("How Far the camera should be when zoomed out, default distance is 3")]
    [SerializeField] float camZoomOutDistance = 1f;

    [Tooltip("How fast the camera should Zoom In and Out")]
    [Range(0.5f, 3f)]
    float camZoomSpeed = 2f;

    private float currCamDist = 3;

    // Cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    [Tooltip("Reference to the Main Camera")]
    [SerializeField] private Transform mainCamTransform;

    private bool bPendingOrientationUpdate = false;

    //The transform that the player will be oriented to (Will be changed on Camera Change)
    private Transform orientationTransform;
    private GameObject orientationObject;

    [Tooltip("To Force player to use Third Person")]
    [SerializeField] private bool bForceUseThirdPerson;

    #endregion

    #region Input Values
    [Header("Player Input Values")]
    public Vector2 moveInput;
    public Vector2 lookInput;
    public bool jump;
    public bool sprint;
    public bool crouch;
    public bool bInteracting;
    public float zoom;

    public bool cursorLocked = true;
	public bool cursorInputForLook = true;
    #endregion

    #region Player Objects (Salt and Batteries)

    public SaltChargeHandler saltChargeHandler;
    
    #endregion

    #region Other Vars

    //Temp #TODO Replace later with the Interactable Script
    private List<InventoryItem> _itemInRange = new List<InventoryItem>();

    [HideInInspector]
    public UnityEvent PlayerInteracted = new UnityEvent();


    private bool isInventoryOpen = false;
    private bool isFlashOn = false;
    private bool isCamMode = false;

    //Reference to Flashlight
    public Flashlight flashlight;
    
    //Reference to Photo Capture Component
    internal PhotoCapture photoCapture;
    private bool isBoardOpen;

    //Reference to VisualEffect
    VisualEffect bloodFx;

    bool bPendingDisableControl = false;
    
    #endregion

    #region Unity Specific Methods

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        nellsAnimator = GetComponent<Animator>();
        photoCapture = GetComponent<PhotoCapture>();
        saltChargeHandler = GetComponent<SaltChargeHandler>();

        bloodFx = GetComponentInChildren<VisualEffect>();
        

        defaultHeight = characterController.height;
        defaultCenter = characterController.center.y;
        crouchCenter = (crouchHeight / 2) + characterController.skinWidth; 
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ogStepOffset = characterController.stepOffset;

        GameManager.Instance.PlayerSpawned(this);

        bloodFx.Stop();

        //Ensuring its set
        mainCamTransform = mainCamTransform == null ? Camera.main.transform : mainCamTransform;

        orientationObject = new GameObject();
        orientationTransform = orientationObject.transform;
        orientationTransform.rotation = mainCamTransform.rotation;

        if(bForceUseThirdPerson)
        {
            orientationTransform = camTarget.transform;
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if(bloodFx)
            bloodFx.Play();

        //#TODO: Remove later just for trial purpose
        if (health <= 0)
            OnCharacterDead?.Invoke();
    }

    private void Update()
    {
        if (bPendingOrientationUpdate)
        {
            UpdateOrientation();
        }

        if (characterController != null && bPlayerHasControl)
        {
            if (bSwimming)
            {
                PlayerSwim();
            }
            else
            {
                PlayerMovement();
            }
        }

        SetAnimatorParams();

        if (Input.GetMouseButtonDown(1)) // Left mouse button
        {
            if (heldItem == null)
            {
                TryPickupItem();
            }
            else
            {
                TryCombineItem();
            }
        }
    }

    private void LateUpdate()
    {
        //#TODO Add condition to check if using third person (Something that can be added in Game Manager)
       
        if(bForceUseThirdPerson)
        {
            CameraRotation();
            CameraZoom();
        }

        if (!bPendingOrientationUpdate && orientationTransform == orientationObject.transform && bPlayerHasControl)
            orientationTransform.rotation = mainCamTransform.rotation;
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = UnityEngine.Random.Range(0, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.position, crouch ? FootstepAudioVolume / 2 : FootstepAudioVolume);
            }
            
            var sound = new Sound(transform.position, soundRange);

            Sounds.MakeSound(sound);
        }
    }

    private void OnAnimatorMove()
    {
        if(bPlayerHasControl)
        {
            if(bGrounded && !bSwimming)
            {
                Vector3 velocity = nellsAnimator.deltaPosition;
                velocity.y = ySpeed * Time.deltaTime;

                characterController.Move(velocity);   
            }
        }
        else
        {
            transform.position += nellsAnimator.deltaPosition;
        }
    }

    #endregion

    #region Player Specifics

    private void PlayerMovement()
    {
        if (!bEnableMovement)
            return;

        Vector3 movDir = new Vector3(moveInput.x, 0, moveInput.y);

        movDir = Quaternion.AngleAxis(orientationTransform.eulerAngles.y, Vector3.up) * movDir;

        float inputMag = Mathf.Clamp01(movDir.magnitude);

        if (sprint && Stamina > 0)
        {
            inputMag *= 2;
            soundRange = runSound;
        }
        else
        {
            soundRange = walkSound;
        }

        PlayerJump();

        nellsAnimator.SetFloat("InputMagnitude", inputMag, 0.05f, Time.deltaTime);   //This is to smmoth out blend value for sharp input changes in WASD

        if (movDir != Vector3.zero)
        {
            bMoving = true;
            
            if (sprint && Stamina > 0)
                DepleteStamina();

            Quaternion toRotation = Quaternion.LookRotation(movDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotSpeed * Time.deltaTime);
        }
        else
        {
            bMoving = false;
             if(GetStamina() < 100)
                GenerateStamina();
        }

        
        if(!bGrounded)
        {
            Vector3 velocity = movDir * inputMag * forwardJumpForce;
            velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime);

        }

    }

    private void PlayerJump()
    {
        if(crouch || bSwimming) return;

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            characterController.stepOffset = ogStepOffset;
            ySpeed = -0.5f;
            bGrounded = true;
            bJumping = false;
            bFalling = false;

            if(bPendingDisableControl)
            {
                SetPlayerHasControl(false);
                bPendingDisableControl = false;
                return;
            }

            if (jump)
            {
                ySpeed = jumpSpeed;
                jump = false;
                bJumping = true;
                
            }
        }
        else
        {
            characterController.stepOffset = 0;
            bGrounded = false;

            if((bJumping && ySpeed < 0) || ySpeed < -4)
            {
                bFalling = true;
            }
        }
    }

    private void PlayerSwim()
    {
        if (!bEnableMovement)
            return;

        Vector3 movDir = new Vector3(moveInput.x, 0, moveInput.y);

        movDir = Quaternion.AngleAxis(orientationTransform.eulerAngles.y, Vector3.up) * movDir;

        float inputMag = Mathf.Clamp01(movDir.magnitude);

        Vector3 velocity = movDir * inputMag * swimSpeed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotSpeed * Time.deltaTime);
        }

    }

    public void UpdateOrientation()
    {
        //If Orientation is overriden by some external transform
        if(GameManager.Instance.OverriddenOrientation() != null)
        {
            orientationTransform = GameManager.Instance.OverriddenOrientation();
            return;
        }

        //If Character is forced in 3rd Person
        if (bForceUseThirdPerson)
        {
            orientationTransform = camTarget.transform;
            return;
        }

        //This is when 
        if (bMoving)
        {
            bPendingOrientationUpdate = true;
            return;
        }

        orientationTransform = orientationObject.transform;
        orientationTransform.rotation = mainCamTransform.rotation;
        bPendingOrientationUpdate = false;

    }

    public void SetPlayerHasControl(bool v)
    {
        //If player is in air let him land
        if(!bGrounded && v == false)
        {
            bPendingDisableControl = true;
            return;
        }

        if (v == false)
        {
            bMoving = false;
        }

        bPlayerHasControl = v;
        characterController.enabled = bPlayerHasControl;
    }

    private void SetAnimatorParams()
    {
        nellsAnimator.SetBool("IsMoving", bMoving);
        nellsAnimator.SetBool("IsJumping", bJumping);
        nellsAnimator.SetBool("IsGrounded", bGrounded);
        nellsAnimator.SetBool("IsFalling", bFalling);
        nellsAnimator.SetBool("IsSwimming", bSwimming);
    }

    private const float _threshold = 0.01f;

    private void CameraRotation()
    {
        // If there is an input and camera position is not fixed
        if (lookInput.sqrMagnitude >= _threshold)
        {
            // Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = 1.0f;

            _cinemachineTargetYaw += lookInput.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += lookInput.y * deltaTimeMultiplier;
        }

        // Clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, camYDown, camYUp);

        // Cinemachine will follow this target
        camTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
    }

    private void CameraZoom()
    {
        if(cineCam == null)
        {
            Debug.Log("Nell Character needs reference to the Cinemachine Virtual Camera for Zoom to work!");
            return;
        }

        currCamDist += zoom * camZoomSpeed * Time.deltaTime;

        currCamDist = Mathf.Clamp(currCamDist, camZoomInDistance, camZoomOutDistance);

        cineCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = currCamDist;
    }

    private void Crouch()
    {
        if (bSwimming)
            return;

        nellsAnimator.SetBool("IsCrouching", crouch);

        if (crouch)
        {
            characterController.center = new Vector3(0f, crouchCenter, 0f);
            characterController.height = crouchHeight;
            soundRange = crouchSound;
            camTarget.transform.position -= new Vector3(0, .5f, 0);
        }
        else
        {
            characterController.center = new Vector3(0f, defaultCenter, 0f);
            characterController.height = defaultHeight;
            soundRange = walkSound;
            camTarget.transform.position += new Vector3(0, .5f, 0);
        }
    }

    public void Teleport(Transform t)
    {
        characterController.enabled = false;
        transform.SetPositionAndRotation(t.position, t.rotation);
        characterController.enabled = true;
    }

    //Set things that are in range and interactable
    internal void SetInventoryItem(InventoryItem inventoryItem)
    {
        _itemInRange.Add(inventoryItem);
    }
    
    internal void RemoveInventoryItem(InventoryItem inventoryItem)
    {
        if (_itemInRange.Contains(inventoryItem))
        {
            _itemInRange.Remove(inventoryItem);
        }
    }

     #region Brandon Stuff

    

 
    void TryPickupItem()
    {
        Input.GetMouseButtonDown(1);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2f))
        {
            if (hit.collider.CompareTag("Item"))
            {
                heldItem = hit.collider.gameObject;
                heldItem.transform.SetParent(holdPosition);
                heldItem.transform.localPosition = Vector3.zero;
                heldItem.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
    
    void TryCombineItem()
    {
        Input.GetMouseButtonDown(1);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2f))
        {
            if (hit.collider.CompareTag("Item") && hit.collider.gameObject != heldItem)
            {
                Item otherItem = hit.collider.GetComponent<Item>();
                Item currentItem = heldItem.GetComponent<Item>();

                if (otherItem.itemType == currentItem.itemType)
                {
                    CombineItems(heldItem, hit.collider.gameObject);
                }
            }
        }
    }
    
    void CombineItems(GameObject item1, GameObject item2)
    {
        // Implement your combination logic here
        Debug.Log("Items combined: " + item1.name + " + " + item2.name);

        // Example: Destroy both items and create a new one
        Destroy(item1);
        Destroy(item2);

        GameObject combinedItem = Instantiate(Resources.Load("CombinedItemPrefab") as GameObject);
        combinedItem.transform.position = holdPosition.position;
        heldItem = combinedItem;
        heldItem.transform.SetParent(holdPosition);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.GetComponent<Rigidbody>().isKinematic = true;
    }
        #endregion
    #endregion

    #region Read Inputs
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            lookInput = value.Get<Vector2>();
        }
    }

    public void OnJump(InputValue value)
    {
        if(!crouch && !bSwimming)
            jump = value.isPressed;
    }

    public void OnSprint(InputValue value)
    {
        sprint = value.isPressed;
    }

    public void OnCrouch(InputValue value)
    {
        if (!bGrounded)
            return;

        crouch = !crouch;
        Crouch();
    }

    public void OnInteract(InputValue value)
    {
        if (!bGrounded)
            return;

        PlayerInteracted?.Invoke();
        //  Debug.Log($"{name} is Interacting");
        if (_itemInRange.Count == 0)
            return;

        if (_itemInRange[_itemInRange.Count - 1] != null)
        {
            _itemInRange[_itemInRange.Count - 1].Interact();
            _itemInRange.RemoveAt(_itemInRange.Count - 1);
        }
    }

    public void OnInteractHold(InputValue value)
    {
        if (!bGrounded)
            return;

        bInteracting = value.isPressed;
    }

    public void OnCamZoom(InputValue value)
    {
        zoom = Mathf.Clamp(value.Get<float>(), -1, 1) * -1;
    }

    public void OnInventory(InputValue value)
    {
        isInventoryOpen = !isInventoryOpen;
        EventBus.Publish(new ToggleInventoryEvent(isInventoryOpen));
    }

    public void OnBoard(InputValue value)
    {
        isBoardOpen = !isBoardOpen;
        EventBus.Publish(new ToggleBoardEvent(isBoardOpen));
    }

    public void OnCamMode(InputValue value)
    {
        isCamMode = !isCamMode;
        //Call to Capture Script Function
    }

    public void OnFlashlight(InputValue value)
    {
        if(flashlight)
        {
            flashlight.ToggleFlashlight();
        }
    }

    public void OnThrowSalt(InputValue value)
    {
        if(bPlayerHasControl)
            saltChargeHandler.ThrowSalt();
    }

    #endregion

    #region HelperMethods
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    #endregion
}
