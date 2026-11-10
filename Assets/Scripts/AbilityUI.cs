using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public Image cooldownFill;
    public float cooldownTimeMax = 5f;

    private float cooldownTimer = 0f;
    [SerializeField] private Abilities_SO abl_SO;
    [SerializeField] private bool isAbilityReady = true; //somuch for removing the bool chec...

    void Start()
    {
        cooldownFill.fillAmount = 1f; // starts READY (full bar)
    }

    void Update()
    {

        if (!isAbilityReady)
        {
            cooldownTimer -= Time.deltaTime;

            float progress = 1f - (cooldownTimer / cooldownTimeMax);
            cooldownFill.fillAmount = progress;

            if (cooldownTimer <= 0f)
            {

                cooldownFill.fillAmount = 1f; // back to FULL (ready)
            }
        }

    }

    public void StartAbiltyClockUI(float maxCOoldown)
    {


        cooldownTimeMax = maxCOoldown;
        cooldownTimer = cooldownTimeMax;
        isAbilityReady = false;
        cooldownFill.fillAmount = 0f; // instantly empty when used
    }
    public void SetUpSwappedCharacterAbiltyRemainingTIme(float remainingTIme, float maxCoolDOwn)
    {
        cooldownTimeMax = maxCoolDOwn;
        cooldownTimer = remainingTIme;
        //Debug.Log($"{gameObject.name}: remaining time: {remainingTIme}, MaxCoolDOwn: {maxCoolDOwn}");
        if (remainingTIme <= 0 || remainingTIme >= maxCoolDOwn)
        {
            cooldownFill.fillAmount = 1f;
            isAbilityReady = true;
            //Debug.Log("in the return");
            return;
        }

        isAbilityReady = false;
        cooldownFill.fillAmount = 1f - (cooldownTimer / cooldownTimeMax);
    }






}