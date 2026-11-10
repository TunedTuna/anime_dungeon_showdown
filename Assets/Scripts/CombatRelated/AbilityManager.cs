using UnityEngine;
//two of these scripts will exist, cannot use instance
public class AbilityManager : MonoBehaviour
{

    [Header("Ability data references")]
    [SerializeField] private Abilities_SO abilitySO_1;
    [SerializeField] private Abilities_SO abilitySO_2;
    [SerializeField] private Abilities_SO abilitySO_3;

    [Header("TimeTimeTime")]

    [SerializeField] private bool abilityReady_1 = true;
    [SerializeField] private bool abilityReady_2 = true;
    [SerializeField] private bool abilityReady_3 = true;
    //ahhhh

    [SerializeField] private float currentTimeAbl_1;
    [SerializeField] private float currentTimeAbl_2;
    [SerializeField] private float currentTimeAbl_3;

    //goofy eventssssss
    public delegate void AbilityHasBeenUsed_1(float maxCooldown);
    public delegate void AbilityHasBeenUsed_2(float maxCooldown);
    public delegate void AbilityHasBeenUsed_3(float maxCooldown);

    public static event AbilityHasBeenUsed_1 OnAbilityHasBeenUsed_1;
    public static event AbilityHasBeenUsed_2 OnAbilityHasBeenUsed_2;
    public static event AbilityHasBeenUsed_3 OnAbilityHasBeenUsed_3;
    [Header("Aiming coords")]
    [SerializeField] private Transform aimDirectionParent;
    [Header("AbilityChildReference")]
    [SerializeField] private GameObject kidObject_1;
    [SerializeField] private GameObject kidObject_2;
    [SerializeField] private GameObject kidObject_3;


    //character swap 
    public delegate void CharacterSwapAbilityUI(float maxCool_1, float maxCool_2, float maxCool_3,
        float current1, float current2, float current3);

    public static event CharacterSwapAbilityUI OnCharacterSwapAbilityUI;


    //interface reference, idk if it'll work

    private void OnEnable()
    {
    
        //Debug.Log("cheese");
        if (abilitySO_1 != null && abilitySO_2 != null && abilitySO_3 != null)
        {
            //SetUpSO();
            PlayerInputController.OnSpecialOne += PlayerInputController_OnSpecialOne;
            PlayerInputController.OnSpecialTwo += PlayerInputController_OnSpecialTwo;
            PlayerInputController.OnSpecialThree += PlayerInputController_OnSpecialThree;
        }


    }

    private void PlayerInputController_OnSpecialThree()
    {
        if (abilityReady_3)
        {
            DoTheAbility_3();
            OnAbilityHasBeenUsed_3?.Invoke(abilitySO_3.CooldownTime);
            abilityReady_3 = false;
            Debug.Log($"DOing abilty 3: {abilitySO_3}");

        }
    }

    private void PlayerInputController_OnSpecialTwo()
    {
        if (abilityReady_2)
        {
            DoTheAbility_2();
            OnAbilityHasBeenUsed_2?.Invoke(abilitySO_2.CooldownTime);
            abilityReady_2 = false;
            Debug.Log($"DOing abilty 2: {abilitySO_2}");

        }
    }

    private void PlayerInputController_OnSpecialOne()
    {
        if (abilityReady_1)
        {
            DoTheAbility_1();
            OnAbilityHasBeenUsed_1?.Invoke(abilitySO_1.CooldownTime);
            abilityReady_1 = false;
            Debug.Log($"DOing abilty 1: {abilitySO_1}");

        }
    }

    private void Update()
    {
        if (abilitySO_1 != null)
        {
            StupidClock(abilitySO_1.CooldownTime, ref currentTimeAbl_1, ref abilityReady_1);

        }
        if (abilitySO_2 != null)
        {
            StupidClock(abilitySO_2.CooldownTime, ref currentTimeAbl_2, ref abilityReady_2);

        }
        if (abilitySO_3 != null)
        {
            StupidClock(abilitySO_3.CooldownTime, ref currentTimeAbl_3, ref abilityReady_3);

        }
    }

    private void DoTheAbility_1() {

        //abilitySO_1.ActivateAbility(); 
        SummonAbilityHitBoxAsChildOfAimPos(abilitySO_1.TestAbiltyTrigger, ref kidObject_1);
    }
    private void DoTheAbility_2() { SummonAbilityHitBoxAsChildOfAimPos(abilitySO_2.TestAbiltyTrigger, ref kidObject_2); }
    private void DoTheAbility_3() { SummonAbilityHitBoxAsChildOfAimPos(abilitySO_3.TestAbiltyTrigger, ref kidObject_3); }

    private void OnDisable()
    {
        PlayerInputController.OnSpecialOne -= PlayerInputController_OnSpecialOne;
        PlayerInputController.OnSpecialTwo -= PlayerInputController_OnSpecialTwo;
        PlayerInputController.OnSpecialThree -= PlayerInputController_OnSpecialThree;
    }

    public void SetUpSO()
    {
        //grabs component from this object and directly updates
        //Debug.Log($"{GetComponent<ICharacterValues>().GetAbility_1()} | {GetComponent<ICharacterValues>().GetAbility_2()} | {GetComponent<ICharacterValues>().GetAbility_3()}");
        abilitySO_1 = GetComponent<ICharacterValues>().GetAbility_1();
        abilitySO_2 = GetComponent<ICharacterValues>().GetAbility_2();
        abilitySO_3 = GetComponent<ICharacterValues>().GetAbility_3();
        abilityReady_1 = true;
        abilityReady_2 = true;
        abilityReady_3 = true;
        currentTimeAbl_1 = abilitySO_1.CooldownTime;
        currentTimeAbl_2 = abilitySO_2.CooldownTime;
        currentTimeAbl_3 = abilitySO_3.CooldownTime;
    }
    public void AbilitySwapUI()
    {
        //pass currentTime + maxCooldown of everyAbility
        if (abilitySO_1 != null && abilitySO_2 != null && abilitySO_3 != null)
        {
            OnCharacterSwapAbilityUI?.Invoke(abilitySO_1.CooldownTime, abilitySO_2.CooldownTime, abilitySO_3.CooldownTime,
                currentTimeAbl_1, currentTimeAbl_2, currentTimeAbl_3);

        }
        else
        {
            Debug.LogWarning("The swapCharacterNeeds data for 1 or both characters!");
        }


    }

    private void StupidClock(float coolDownMax, ref float currentTIme, ref bool temp)
    {
        if (!temp)//if  ability not rady
        {

            currentTIme -= Time.deltaTime;
            if (currentTIme <= 0)
            {
                temp = true;
                currentTIme = coolDownMax;
            }
        }
    }
    private void SummonAbilityHitBoxAsChildOfAimPos(GameObject temp,ref GameObject kidRefernce)
    {
        if (kidRefernce == null)
        {
            
            kidRefernce = Instantiate(temp, aimDirectionParent);
            kidRefernce.transform.position=new Vector3(0,1,4);
            kidRefernce.GetComponent<IAbilities>().ActivateAbility();
        }
        else
        {
            kidRefernce.GetComponent<IAbilities>().ActivateAbility();
        }
       

    }
}
