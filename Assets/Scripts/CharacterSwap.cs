using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// holds the logic for swapping between two characters
/// -because it has reference to both characters, use it to simplify inputs?
/// --because of events, this can get dumb really really fast
/// </summary>
public class CharacterSwap : MonoBehaviour
{
    public static CharacterSwap instance { get; private set; }//prob regret this
    [Header("Characters")]
    public GameObject characterA;
    public string characterNameA;
    public GameObject characterB;
    public string characterNameB;

    //dead character refernce
    [Header("DeathReference")]
    [SerializeField] private bool isTeammateDead = false;
    [SerializeField] private GameObject deadTeammate_GameObject;

    [Header("Temp Character Data")]
    [SerializeField] private CharacterData_SO CharacterData_A;
    [SerializeField] protected CharacterData_SO CharacterData_B;


    [Header("Settings")]
    //public KeyCode swapKey = KeyCode.F;
    public bool startWithCharacterA = true;

    [SerializeField] private GameObject activeCharacter;
    private Camera mainCamera;
    private CameraFollow camFollow;

    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
        if (PlayerInputController.instance != null)
        {

            PlayerInputController.instance.OnCharactersSwapped += PlayerInputController_OnCharactersSwapped;
            PlayerHealth.OnCharacterHasDied += PlayerHealth_OnCharacterHasDied;
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
            characterNameA = characterA.GetComponent<ICharacterValues>().GetCharacterName();
            characterNameB = characterB.GetComponent<ICharacterValues>().GetCharacterName();
            activeCharacter.GetComponent<PlayerHealth>().PassOnUIdata();
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
        if (!isTeammateDead)
        {

            SwapCharacter();
        }
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
        activeCharacter.GetComponent<PlayerHealth>().PassOnUIdata();

        activeCharacter.GetComponent<AbilityManager>().AbilitySwapUI();

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
                PlayerInputController.instance.OnCharactersSwapped -= PlayerInputController_OnCharactersSwapped;
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

    public void DisablePlayerMovement()
    {
        activeCharacter.GetComponent<PlayerMovement>().enabled = false;

    }
    public void EnablePlayerMovement()
    {
        activeCharacter.GetComponent<PlayerMovement>().enabled = true;

    }

    public void RefreshCharacterData(CharacterData_SO a, CharacterData_SO b)
    {
        //called After change from CHaracter Selection
        CharacterData_A = a;
        CharacterData_B = b;
        //hmm
        characterA.GetComponent<ICharacterValues>().CharacterSelectionDataChange();
        characterB.GetComponent<ICharacterValues>().CharacterSelectionDataChange();
        //ahh
        characterA.GetComponent<AbilityManager>().SetUpSO();
        characterB.GetComponent<AbilityManager>().SetUpSO();
        //oof
        activeCharacter.GetComponent<PlayerHealth>().PassOnUIdata();
    }
    public (CharacterData_SO, CharacterData_SO) GetCurrentPart()
    {

        return (CharacterData_A, CharacterData_B);
    }
    //Death
    private void PlayerHealth_OnCharacterHasDied(GameObject character)
    {
        //if teammate is not dead, now they are. if a teammate is already dead and this is heard, "GAME OVER MAN"
        if (!isTeammateDead)
        {
            SwapCharacter(); //call this before the bool check so we can still use it
            isTeammateDead = true;
            deadTeammate_GameObject = character;

        }
        else
        {

            //fire an event that the Game is Over
            //switch to UI input/ disable player input
            //pull up GameOver screen
            //just have the player confirm to go back to MainMenu (display some stats ? they could look at?)

        }
    }


    //---WARNING--- this locks A and B into those slots, for testing---WARNING----
    //(characerA script =melee, character B script= ranged, each script calls respective Getter)
    public CharacterData_SO GetSlotData_A()
    {
        return CharacterData_A;
    }
    public CharacterData_SO GetSlotData_B()
    {
        return CharacterData_B;
    }




}