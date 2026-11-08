using UnityEngine;

public class InputSwapHandler : MonoBehaviour
{
    public static InputSwapHandler Instance { get; private set; }
    [SerializeField] private PlayerInputController playerInput;
    [SerializeField] private UserInterfaceInputController userInterfaceInput;
    private void Awake()
    {
        Instance = this;
        CheckIfInGamePlayScene();
    }
    public void EnableUserInterfaceInput()
    {
        userInterfaceInput.enabled = true;
        playerInput.enabled = false;
       

        //--invoke ya boy GlobalEventManager---
        GlobalEventManager.Fire_GoingToInput_UI();
    }
    public void EnablePlayerController()
    {
        playerInput.enabled = true;
        userInterfaceInput.enabled = false;
        

        //---invoke ya boy GlobalEventManager---
        GlobalEventManager.Fire_GoingToInput_Player();
    }

    private void CheckIfInGamePlayScene()
    {
        if (CharacterSwap.instance != null)
        {
            EnablePlayerController();
        }
    }
}
