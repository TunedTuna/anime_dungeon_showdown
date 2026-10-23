using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// holds the logic for swapping between two characters
/// -because it has reference to both characters, use it to simplify inputs?
/// --because of events, this can get dumb really really fast
/// </summary>
public class CharacterSwap : MonoBehaviour
{
    [Header("Characters")]
    public GameObject characterA;
    public GameObject characterB;

    [Header("Settings")]
    public KeyCode swapKey = KeyCode.F;
    public bool startWithCharacterA = true;

    [SerializeField] private GameObject activeCharacter;
    private Camera mainCamera;
    private CameraFollow camFollow;

    void Start()
    {
        if (PlayerInputController.instance != null)
        {

            PlayerInputController.instance.OnCharactersSwapped += PlayerInputController_OnCharactersSwapped;
            //---input for specials not implemented yet---
            //PlayerInputController.instance.OnBasicAttack += PlayerInputController_OnBasicAttack;
            //PlayerInputController.instance.OnSpecialOne += PlayerInputController_OnSpecialOne;
            //PlayerInputController.instance.OnSpecialTwo += PlayerInputController_OnSpecialTwo;
            //PlayerInputController.instance.OnSpecialThree += PlayerInputController_OnSpecialThree;
        }
        else
        {
            Debug.LogWarning("ey, the player input script is probably missing!");
        }
        activeCharacter = startWithCharacterA ? characterA : characterB;

        if (characterA != null && characterB != null)
        {
            characterA.SetActive(startWithCharacterA);
            characterB.SetActive(!startWithCharacterA);
        }

        mainCamera = Camera.main;
        if (mainCamera != null)
            camFollow = mainCamera.GetComponent<CameraFollow>();

        if (camFollow != null && activeCharacter != null)
        {
            camFollow.SetTarget(activeCharacter.transform);
        }
    }

    private void PlayerInputController_OnSpecialThree(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void PlayerInputController_OnSpecialTwo(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void PlayerInputController_OnSpecialOne(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void PlayerInputController_OnBasicAttack(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void PlayerInputController_OnCharactersSwapped(object sender, System.EventArgs e)
    {
        SwapCharacter();
    }



    void SwapCharacter()
    {
        if (characterA == null || characterB == null)
        {
            Debug.LogWarning("Character references missing!");
            return;
        }

        GameObject nextCharacter = (activeCharacter == characterA) ? characterB : characterA;

        Vector3 currentPos = activeCharacter.transform.position;
        Quaternion currentRot = activeCharacter.transform.rotation;

        activeCharacter.SetActive(false);
        nextCharacter.SetActive(true);
        nextCharacter.transform.SetPositionAndRotation(currentPos, currentRot);

        activeCharacter = nextCharacter;

        if (camFollow != null)
            camFollow.SetTarget(activeCharacter.transform);

        Debug.Log("Swapped to: " + activeCharacter.name);
    }
    //todo, have events to call the current active character's ability since this script already has access to both of'em

    private void OnDisable()
    {
        {
            if (PlayerInputController.instance != null)
            {
                //don't forget to dislike and unsubscribe!
                //PlayerInputController.instance.OnCharactersSwapped -= PlayerInputController_OnCharactersSwapped;
                //PlayerInputController.instance.OnBasicAttack -= PlayerInputController_OnBasicAttack;
                //PlayerInputController.instance.OnSpecialOne -= PlayerInputController_OnSpecialOne;
                //PlayerInputController.instance.OnSpecialTwo -= PlayerInputController_OnSpecialTwo;
                //PlayerInputController.instance.OnSpecialThree += PlayerInputController_OnSpecialThree;
            }
            else
            {
                Debug.LogWarning("Ey! the player input script is probably missing!");
            }
        }

    }


}