using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public Image cooldownFill;
    public float cooldownTime = 5f;

    private float cooldownTimer = 0f;
    [SerializeField] private Abilities_SO abl_SO;

    void Start()
    {
        cooldownFill.fillAmount = 1f; // starts READY (full bar)
    }

    void Update()
    {
      

        cooldownTimer -= Time.deltaTime;

        float progress = 1f - (cooldownTimer / cooldownTime);
        cooldownFill.fillAmount = progress;

        if (cooldownTimer <= 0f)
        {
      
            cooldownFill.fillAmount = 1f; // back to FULL (ready)
        }
    }

    public void StartAbiltyClockUI(float maxCOoldown)
    {


        cooldownTime=maxCOoldown;
        cooldownTimer = cooldownTime;

        cooldownFill.fillAmount = 0f; // instantly empty when used
    }
    public void SetUpSwappedCharacterAbiltyRemainingTIme(float remainingTIme, float maxCoolDOwn)
    {
        if (remainingTIme >= maxCoolDOwn)
            return;
        cooldownTime = maxCoolDOwn;
        cooldownTimer = remainingTIme;
    }






}