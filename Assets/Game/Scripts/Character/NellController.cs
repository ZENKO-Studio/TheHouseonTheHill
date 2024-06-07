
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(AudioSource))]
public class NellController : CharacterBase
{

    CharacterController characterController;
    Animator animator;

    #region Character Control Values
    [Header("Character Controls")]
    
    public bool bEnableMovement = true;
    
    [SerializeField] float rotSpeed = 5f;

    //Can be used if jumping required
    [SerializeField] float jumpSpeed = 4f;

    float ogStepOffset;
    float ySpeed = 0f;

    [SerializeField] float crouchHeight = 1.28f;
    private float crouchCenter;
    private float defaultHeight;
    private float defaultCenter;
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

    #region Input Values
    [Header("Player Input Values")]
    public Vector2 moveInput;
    public Vector2 lookInput;
    public bool jump;
    public bool sprint;
    public bool crouch;

    public bool cursorLocked = true;
	public bool cursorInputForLook = true;
    #endregion

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        defaultHeight = characterController.height;
        defaultCenter = characterController.center.y;
        crouchCenter = (crouchHeight / 2) + characterController.skinWidth; 
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ogStepOffset = characterController.stepOffset;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        //#TODO: Remove later just for trial purpose
        if (health <= 0)
            SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (characterController != null)
        {
            PlayerMovement();
        }
    }

    private void PlayerMovement()
    {
        if (!bEnableMovement)
            return;

        Vector3 movDir = new Vector3(moveInput.x, 0, moveInput.y);
        float inputMag = Mathf.Clamp01(movDir.magnitude);

        if (sprint)
        {
            inputMag *= 2;
            soundRange = runSound;
        }
        else
        {
            soundRange = walkSound;
        }

        PlayerJump();

        animator.SetFloat("InputMagnitude", inputMag, 0.05f, Time.deltaTime);   //This is to smmoth out blend value for sharp input changes in WASD

        if (movDir != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);

            Quaternion toRotation = Quaternion.LookRotation(movDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

    }

    private void PlayerJump()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            characterController.stepOffset = ogStepOffset;
            ySpeed = -0.5f;

            if (jump)
            {
                ySpeed = jumpSpeed;
                jump = false;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }
    }

    private void Crouch()
    {
        animator.SetBool("IsCrouching", crouch);

        if (crouch)
        {
            characterController.center = new Vector3(0f, crouchCenter, 0f);
            characterController.height = crouchHeight;
            soundRange = crouchSound;
        }
        else
        {
            characterController.center = new Vector3(0f, defaultCenter, 0f);
            characterController.height = defaultHeight;
            soundRange = walkSound;
        }
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {

        Debug.Log("Footstep");
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.position, crouch ? FootstepAudioVolume / 2 : FootstepAudioVolume);
            }
            
            var sound = new Sound(transform.position, soundRange);

            Sounds.MakeSound(sound);
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = animator.deltaPosition;
        velocity.y = ySpeed * Time.deltaTime;

        characterController.Move(velocity);

        
    }

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
        jump = value.isPressed;
    }

    public void OnSprint(InputValue value)
    {
        sprint = value.isPressed;
    }

    public void OnCrouch(InputValue value)
    {
        crouch = !crouch;
        Crouch();
    }

    

    public void OnInteract(InputValue value)
    {
        Debug.Log($"{name} is Interacting");
    }
    #endregion
}
