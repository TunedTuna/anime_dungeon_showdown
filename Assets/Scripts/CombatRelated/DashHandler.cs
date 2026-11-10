using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
/// <summary>
/// dashing
/// -if dash && !move -> move 'foreward'
/// -if dash && move -> move in that direction
/// -if dash && !move input->stop
/// -if dash && move -> max movement/velocity
/// </summary>
public class DashHandler : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 10f;
    public float acceleration = 20f;
    public float airControl = 0.5f;
    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    [Header("Knockback")]
    public float knockbackDecay = 20f;

    private CharacterController controller;

    private bool dashPressed;
    private bool canDash = true;
    private bool isDashing = false;
    [SerializeField] private bool isGrounded = true;

    [SerializeField] private Vector3 velocity;
    private Vector3 externalVelocity;

    [SerializeField] private float dashTimeLeft;
    [HideInInspector] private Vector3 dashDirection;

    private bool usingNewInput;
    private Vector2 moveInput;

    [Header("DashTestVar")]
    [SerializeField] private bool isTestingVelocity_Z = false;
    [SerializeField] private bool isZedReset = false;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        ReadInput();            //should be on MainObject
        HandleDash();           //ditto
        //TestVelocity_Z();
        ApplyFinalMovement();
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

    void StartDash()
    {
        isDashing = true;
        canDash = false;
        dashTimeLeft = dashDuration;

        Vector3 moveDir = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;
        dashDirection = moveDir.sqrMagnitude < 0.01f ? transform.forward : moveDir.normalized; //points at direction
        DashMovementAction();


    }

    void EndDash()
    {
        isDashing = false;
        dashTimeLeft = 0f;
        //reset velocity, if continuing to move after dash: max speed, otherwise STOP moving
        velocity=Vector3.zero;
        Invoke(nameof(ResetDash), dashCooldown);
    }
    private void DashMovementAction()
    {
      
       
        velocity.x = dashDirection.x *dashSpeed;
        velocity.z = dashDirection.z * dashSpeed;
    }
    void ResetDash() => canDash = true;

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
    void ApplyFinalMovement()
    {
        externalVelocity = Vector3.MoveTowards(externalVelocity, Vector3.zero, knockbackDecay * Time.deltaTime);
        Vector3 finalVelocity = velocity + externalVelocity;
        controller.Move(finalVelocity * Time.deltaTime);
    }
    private void TestVelocity_Z()
    {
        if (isTestingVelocity_Z)
        {
            velocity.z = 20f;
            isZedReset = true;
        }
        else
        {
            if (isZedReset)
            {
                velocity = Vector3.zero;
                isZedReset = false;
            }
        }

    }
    void OnDrawGizmosSelected()
    {//ai
        // Ensure the game object exists in the scene and is selected
        if (transform == null) return;

        // Set the gizmo color to orange (Unity doesn't have a built-in Color.orange)
        Gizmos.color = new Color(1.0f, 0.5f, 0.0f);

        // Draw a line starting from the character's position extending in the dash direction
        Gizmos.DrawLine(transform.position, transform.position + (dashDirection * 2f));
    }

}
