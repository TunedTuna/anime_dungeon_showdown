using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Image hpBar;
    [SerializeField] private float currentCharacterMaxHP;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private bool isTestingFixedCharacter;

    //could prob just use 1 GameObject then exrat all scripts instead of manually dragging them :/
    [SerializeField] private PlayerHealth testCHaracterHpStuff;
    [SerializeField] private CharacterA CharacterA;
    
    private void Start()
    {
        if (isTestingFixedCharacter)
        {
            PlayerHealth.OnPlayerDamaged += PlayerHealth_OnPlayerDamaged;//will break bc 2 characters
            DecorateWithTestCharacter();

        }
        //decorate UI to match current character
    }

    private void PlayerHealth_OnPlayerDamaged(float hp)
    {
        hpBar.fillAmount = hp / currentCharacterMaxHP; //lower's hp
    }
    private void DecorateWithTestCharacter()
    {
        currentCharacterMaxHP = testCHaracterHpStuff.maxHealth;
        characterName.text = CharacterA.characterName;
        FullHealHpBar();
        
    }

    private void FullHealHpBar() {
        hpBar.fillAmount = 1f;
    }
}
