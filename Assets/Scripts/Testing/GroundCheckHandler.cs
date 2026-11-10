using UnityEngine;

public class GroundCheckHandler : MonoBehaviour
{
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [SerializeField] private Vector3 velocity;
    private Vector3 externalVelocity;

    [SerializeField] private bool isGrounded;
    void Update()
    {
        CheckGrounded();        //i guess main object
    }
    void CheckGrounded()
    {
        isGrounded = groundCheck != null &&
                     Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
    }
}
