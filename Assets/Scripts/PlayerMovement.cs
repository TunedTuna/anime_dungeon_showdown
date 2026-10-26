using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 10f;
    public float acceleration = 20f;
    public float airControl = 0.5f;

    [Header("Dash")]
    public float dashSpeed = 40f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public AudioClip dashSFX;
    public GameObject dashVFX;
    [SerializeField] private bool isInvincible = false;

    [Header("Knockback")]
    public float knockbackDecay = 20f;
    [SerializeField] private KnockBackHandler knockBackHandler;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Gravity & Jump")]
    public float gravity = -20f;
    public float jumpHeight = 2f;
    [Header("Warp?")]


    [Header("Animation")]
    public Animator animator;

    private CharacterController controller;
    private AudioSource audioSource;

    [Header("Private Values")]
    [SerializeField] private Vector3 velocity;
    private Vector3 externalVelocity;

    [SerializeField] private bool isGrounded;
    private bool canDash = true;
    private bool isDashing = false;
    [SerializeField] private float dashTimeLeft;
    private Vector3 dashDirection;

    private Vector2 moveInput;
    private bool dashPressed;
    private bool usingNewInput;

    void Start()
    {
        knockBackHandler = GetComponent<KnockBackHandler>();
        controller = GetComponent<CharacterController>();
        controller.stepOffset = 0.1f;

        if (animator == null)
            animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

      
        usingNewInput = Keyboard.current != null || Gamepad.current != null;
    }

    void Update()
    {
        ReadInput();            //should be on MainObject
        CheckGrounded();        //i guess main object
        HandleDash();           //ditto
        
        HandleMovement();       //main
        ApplyGravity();
        ApplyFinalMovement();
        UpdateAnimation();
    }
    void CheckGrounded()
    {
        isGrounded = groundCheck != null &&
                     Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = 0f;
    }

    void HandleDash()
    {
        if (dashPressed && canDash && !isDashing)
            StartDash();

        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0f)
                EndDash();
        }
    }

    void HandleMovement()
    {
        if (isDashing) return;

        Vector3 moveDirection = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;
        Vector3 targetVelocity = moveDirection * maxSpeed;

        float currentAccel = isGrounded ? acceleration : acceleration * airControl;
        Vector3 horizontalVel = new Vector3(velocity.x, 0f, velocity.z);
        horizontalVel = Vector3.MoveTowards(horizontalVel, targetVelocity, currentAccel * Time.deltaTime);
        velocity.x = horizontalVel.x;
        velocity.z = horizontalVel.z;
        
    }

    void ApplyGravity()
    {
        if (!isDashing)
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }

    void ApplyFinalMovement()
    {
        if (knockBackHandler.IsKnockBackActive() || controller.enabled==false) return;//if knockback is active OR controller is off. skipp
        //Debug.Log("knock back activity: "+(knockBackHandler.IsKnockBackActive() + " || controller activiry: " + controller.enabled));
        externalVelocity = Vector3.MoveTowards(externalVelocity, Vector3.zero, knockbackDecay * Time.deltaTime);
        Vector3 finalVelocity = velocity + externalVelocity;
        controller.Move(finalVelocity * Time.deltaTime);
    }

    void UpdateAnimation()
    {
        if (animator != null)
        {
            float speed = new Vector3(velocity.x, 0f, velocity.z).magnitude;
            animator.SetFloat("Speed", speed);
            animator.SetBool("IsGrounded", isGrounded);
        }
    }

    void ReadInput()
    {
        if (usingNewInput)
        {
            var keyboard = Keyboard.current;
            var gamepad = Gamepad.current;

            moveInput = Vector2.zero;

            if (keyboard != null)
            {
                if (keyboard.aKey.isPressed) moveInput.x = -1f;
                if (keyboard.dKey.isPressed) moveInput.x = 1f;
                if (keyboard.wKey.isPressed) moveInput.y = 1f;
                if (keyboard.sKey.isPressed) moveInput.y = -1f;
            }

            if (gamepad != null)
            {
                Vector2 stick = gamepad.leftStick.ReadValue();
                if (stick.magnitude > 0.1f)
                    moveInput = stick;
            }

            dashPressed = (keyboard != null && keyboard.leftShiftKey.wasPressedThisFrame) ||
                          (gamepad != null && gamepad.rightShoulder.wasPressedThisFrame);
        }
        else
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            moveInput = new Vector2(x, z);
            dashPressed = Input.GetKeyDown(KeyCode.LeftShift);
        }
    }





    void StartDash()
    {
        isDashing = true;
        canDash = false;
        dashTimeLeft = dashDuration;
        isInvincible = true;
        //do an animation call, blink white?

        Vector3 moveDir = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;
        dashDirection = moveDir.sqrMagnitude < 0.01f ? transform.forward : moveDir.normalized;

        velocity.x = dashDirection.x * dashSpeed;
        velocity.z = dashDirection.z * dashSpeed;

        if (dashSFX != null)
            audioSource.PlayOneShot(dashSFX);

        if (dashVFX != null)
        {
            GameObject vfx = Instantiate(dashVFX, transform.position, transform.rotation);
            Destroy(vfx, 2f);
        }
    }

    void EndDash()
    {
        isDashing = false;
        dashTimeLeft = 0f;
        isInvincible = false;
        velocity = Vector3.zero;//todo: fix this to keep max base speed velocity after input and moving
        Invoke(nameof(ResetDash), dashCooldown);
    }

    void ResetDash() => canDash = true;

    public void AddExternalForce(Vector3 force) => externalVelocity += force;

    public bool IsDashing() => isDashing;
    public void WarpPlayer(Transform newPoint)
    {
        controller.enabled = false;
        gameObject.transform.position=newPoint.position;
        controller.enabled = true;

    }
    public bool CheckIfInvincible()
    {
        return isInvincible;
    }
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }

}