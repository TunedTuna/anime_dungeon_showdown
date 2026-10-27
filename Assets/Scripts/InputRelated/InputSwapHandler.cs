using UnityEngine;

public class InputSwapHandler : MonoBehaviour
{
    public static InputSwapHandler Instance { get; private set; }
    [SerializeField] private PlayerInputController playerInput;
    [SerializeField] private UserInterfaceInputController userInterfaceInput;

    private void Awake()
    {
        Instance = this;
    }
    public void EnableUserInterfaceInput()
    {
        userInterfaceInput.enabled = true;
        playerInput.enabled = false;
        CharacterSwap.instance.DisablePlayerMovement();
    }
    public void EnablePlayerController()
    {
        playerInput.enabled = true;
        userInterfaceInput.enabled = false;
        CharacterSwap.instance.EnablePlayerMovement();
    }
}
