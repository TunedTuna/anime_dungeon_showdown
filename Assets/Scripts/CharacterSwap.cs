using Unity.VisualScripting;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
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
    private void OnEnable()
    {
        PlayerInputController.OnCharactersSwapped += PlayerInputController_OnCharactersSwapped;
        GlobalEventManager.OnCharacterHasDied += GlobalEventManager_OnCharacterHasDied;
        GlobalEventManager.OnLevelLoaded += GlobalEventManager_OnLevelLoaded;
        GlobalEventManager.OnHealActiveCharacter += GlobalEventManager_OnHealActiveCharacter;
        //Debug.Log("Cheese");

        if (activeCharacter != null)
        {
            EnablePlayerMovement();

        }
    }

    private void GlobalEventManager_OnHealActiveCharacter(float obj)
    {
        activeCharacter.GetComponent<PlayerHealth>().RestoreHp(obj);
    }

    private void GlobalEventManager_OnLevelLoaded((GameObject, GameObject) obj)
    {
        characterA = obj.Item1;
        characterB = obj.Item2;
        ForceStart();
        RefreshCharacterData(CharacterData_A, CharacterData_B);
        Debug.Log("inPlayer swap!");
    }

    private void PlayerInputController_OnCharactersSwapped()
    {
        if (!isTeammateDead)
        {

            SwapCharacter();
        }
    }

    void Start()
    {

        StartCopyPasta();
    }

    private void ForceStart()
    {
        StartCopyPasta();
    }

    private void StartCopyPasta()
    {
        activeCharacter = startWithCharacterA ? characterA : characterB;
        //subCharacter = 
        EnablePlayerMovement();
        if (characterA != null && characterB != null)
        {
            characterA.SetActive(startWithCharacterA);
            characterB.SetActive(!startWithCharacterA);
            characterNameA = characterA.GetComponent<ICharacterValues>().GetCharacterName();
            characterNameB = characterB.GetComponent<ICharacterValues>().GetCharacterName();
            activeCharacter.GetComponent<PlayerHealth>().PassOnUIdata();
            HealthBar_SubMemberUI.Instance.DecorateSub(characterB.GetComponent<PlayerHealth>().GetCurrentHp(),characterB.GetComponent<PlayerHealth>().GetMaxHp());
        }

        mainCamera = Camera.main;
        if (mainCamera != null)
            camFollow = mainCamera.GetComponent<CameraFollow>();

        if (camFollow != null && activeCharacter != null)
        {
            camFollow.SetTarget(activeCharacter.transform);
        }
    }


    void SwapCharacter()
    {
        if (characterA == null || characterB == null)
        {
            Debug.LogWarning("Character references missing!");
            return;
        }
        HealthBar_SubMemberUI.Instance.DecorateSub(activeCharacter.GetComponent<PlayerHealth>().GetCurrentHp(), activeCharacter.GetComponent<PlayerHealth>().GetMaxHp());
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


        //don't forget to dislike and unsubscribe!
        PlayerInputController.OnCharactersSwapped -= PlayerInputController_OnCharactersSwapped;
        GlobalEventManager.OnCharacterHasDied -= GlobalEventManager_OnCharacterHasDied;
        GlobalEventManager.OnLevelLoaded -= GlobalEventManager_OnLevelLoaded;
        GlobalEventManager.OnHealActiveCharacter -= GlobalEventManager_OnHealActiveCharacter;
        DisablePlayerMovement();



    }

    public void DisablePlayerMovement()
    {
        if (activeCharacter == null) return;
        activeCharacter.GetComponent<PlayerMovement>().enabled = false;

    }
    public void EnablePlayerMovement()
    {
        if (activeCharacter == null) return;
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
        //ugh
        characterA.GetComponent<VisualHelper>().SpriteChangeFromCharacterChange(a.SpritePreview);
        characterB.GetComponent<VisualHelper>().SpriteChangeFromCharacterChange(b.SpritePreview);
        //oof
        activeCharacter.GetComponent<PlayerHealth>().PassOnUIdata();

        GlobalEventManager.Fire_OnTeamFilled();
    }
    public (CharacterData_SO, CharacterData_SO) GetCurrentPart()
    {

        return (CharacterData_A, CharacterData_B);
    }
    //Death
    private void GlobalEventManager_OnCharacterHasDied(GameObject obj)
    {
        //if teammate is not dead, now they are. if a teammate is already dead and this is heard, "GAME OVER MAN"
        if (!isTeammateDead)
        {
            Debug.Log("A teammate has died");
            SwapCharacter(); //call this before the bool check so we can still use it
            isTeammateDead = true;
            deadTeammate_GameObject = obj;

        }
        else
        {

            //fire an event that the Game is Over
            //switch to UI input/ disable player input
            //pull up GameOver screen
            //just have the player confirm to go back to MainMenu (display some stats ? they could look at?)
            GlobalEventManager.Fire_GameOver();


        }
    }
    //listen to event for GOing toUI


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