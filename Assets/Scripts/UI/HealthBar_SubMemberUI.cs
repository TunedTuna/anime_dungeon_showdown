using UnityEngine;
using UnityEngine.UI;
public class HealthBar_SubMemberUI : MonoBehaviour
{
    public static HealthBar_SubMemberUI Instance { get; private set; }

    [SerializeField] private Image hpBar;
    [SerializeField] private float activeCharacterMaxHP;

    private void Awake()
    {
        Instance = this;
    }
    public void DecorateSub(float currentHP, float MaxHp)
    {
        hpBar.fillAmount = currentHP / MaxHp;
    }
}
