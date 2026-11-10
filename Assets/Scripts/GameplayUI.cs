using UnityEngine;

public class GameplayUI : MonoBehaviour,IUserInterface
{
    public static GameplayUI Instance { get; private set; }
    public AbilityUI ability1;
    public AbilityUI ability2;
    public AbilityUI ability3;
    public AbilityUI ability4;

    [SerializeField] private GameObject AmmoUI;


    //UI should listen to an event
    //current set up has the abilites fixed positions...
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        SubscribeToAbilitiesPage();
        AbilityManager.OnCharacterSwapAbilityUI += AbilityManager_OnCharacterSwapAbilityUI;
        GlobalEventManager.OnGoingToInput_Player += GlobalEventManager_OnGoingToInput_Player;
        GlobalEventManager.OnGoingToInput_UI += GlobalEventManager_OnGoingToInput_UI;
        GlobalEventManager.OnGameOver += GlobalEventManager_OnGameOver;
        GlobalEventManager.OnToggleAmmoUI += GlobalEventManager_OnToggleAmmoUI;
        GlobalEventManager.OnRangedCharacterDecorateUI += GlobalEventManager_OnRangedCharacterDecorateUI;
    }

    private void GlobalEventManager_OnRangedCharacterDecorateUI(int obj)
    {
        AmmoUI.GetComponent<RangedCharacterAmmoUI>().SetRangedInfoUI(obj);
    }

    private void GlobalEventManager_OnToggleAmmoUI(bool obj)
    {

        AmmoUI.SetActive(obj);
        
    }

    private void GlobalEventManager_OnGameOver()
    {
       HideUI();
    }

    private void GlobalEventManager_OnGoingToInput_UI()
    {
        HideUI(); //child can hide itself, but not show, hence parent relation ship
    }

    private void GlobalEventManager_OnGoingToInput_Player()
    {
        
    }

    private void AbilityManager_OnCharacterSwapAbilityUI(float maxCool_1, float maxCool_2, float maxCool_3, float current1, float current2, float current3)
    {
        ability1.SetUpSwappedCharacterAbiltyRemainingTIme(current1, maxCool_1);
        ability2.SetUpSwappedCharacterAbiltyRemainingTIme(current2, maxCool_2);
        ability3.SetUpSwappedCharacterAbiltyRemainingTIme(current3 , maxCool_3);
    }



    public void HideUI()//now part of contract
    {
        gameObject.SetActive(false);
    }
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

    private void SubscribeToAbilitiesPage()
    {
        AbilityManager.OnAbilityHasBeenUsed_1 += AbilityManager_OnAbilityHasBeenUsed_1;
        AbilityManager.OnAbilityHasBeenUsed_2 += AbilityManager_OnAbilityHasBeenUsed_2;
        AbilityManager.OnAbilityHasBeenUsed_3 += AbilityManager_OnAbilityHasBeenUsed_3;
    }

    private void UnsubscribeToAbilitiesPage()
    {
        AbilityManager.OnAbilityHasBeenUsed_1 -= AbilityManager_OnAbilityHasBeenUsed_1;
        AbilityManager.OnAbilityHasBeenUsed_2 -= AbilityManager_OnAbilityHasBeenUsed_2;
        AbilityManager.OnAbilityHasBeenUsed_3 -= AbilityManager_OnAbilityHasBeenUsed_3;
    }

    private void AbilityManager_OnAbilityHasBeenUsed_3(float maxCooldown)
    {
        ability3.StartAbiltyClockUI(maxCooldown);
    }

    private void AbilityManager_OnAbilityHasBeenUsed_2(float maxCooldown)
    {
        ability2.StartAbiltyClockUI(maxCooldown);
    }

    private void AbilityManager_OnAbilityHasBeenUsed_1(float maxCooldown)
    {
        ability1.StartAbiltyClockUI(maxCooldown);
    }

  
    private void OnDisable()
    {
        UnsubscribeToAbilitiesPage();
        AbilityManager.OnCharacterSwapAbilityUI -= AbilityManager_OnCharacterSwapAbilityUI;
        GlobalEventManager.OnGoingToInput_Player -= GlobalEventManager_OnGoingToInput_Player;
        GlobalEventManager.OnGoingToInput_UI -= GlobalEventManager_OnGoingToInput_UI;
        GlobalEventManager.OnGameOver -= GlobalEventManager_OnGameOver;
        GlobalEventManager.OnToggleAmmoUI -= GlobalEventManager_OnToggleAmmoUI;
        GlobalEventManager.OnRangedCharacterDecorateUI -= GlobalEventManager_OnRangedCharacterDecorateUI;
    }
}