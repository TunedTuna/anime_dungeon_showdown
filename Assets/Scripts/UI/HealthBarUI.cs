using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public static HealthBarUI Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Image hpBar;
    [SerializeField] private float activeCharacterMaxHP;
    [SerializeField] private float activeCharacterCurrentHP;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private bool isTestingFixedCharacter;

    //could prob just use 1 GameObject then exrat all scripts instead of manually dragging them :/
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (isTestingFixedCharacter)
        {
            PlayerHealth.OnPlayerDamaged += PlayerHealth_OnPlayerDamaged;//will break bc 2 characters
          

        }
        //decorate UI to match current character
    }

    private void PlayerHealth_OnPlayerDamaged(float hp)
    {
        hpBar.fillAmount = hp / activeCharacterMaxHP; //lower's hp
    }
    public void DecorateWithTestCharacter(string inComingName, float incomingMaxHp,float incomingCurrentHP)
    {
        activeCharacterMaxHP = incomingMaxHp;
        activeCharacterCurrentHP = incomingCurrentHP;
        characterName.text = inComingName;
        PlayerHealth_OnPlayerDamaged(incomingCurrentHP);


    }

    private void FullHealHpBar() {
        hpBar.fillAmount = 1f;
    }
    private void OnDisable()
    {
        if (isTestingFixedCharacter)
        {
            PlayerHealth.OnPlayerDamaged -= PlayerHealth_OnPlayerDamaged;//will break bc 2 characters


        }
    }
}
