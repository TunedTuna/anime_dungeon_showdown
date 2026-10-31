using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class Abilities_SO :ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float cooldownTime;

    [Header("Statuses/conditons")]
    [SerializeField] private bool canBurn;
    [SerializeField] private bool canStun;
    [SerializeField] private bool canBleed;
    [SerializeField] private bool canSlow;

    [Header("UI stuff")]
    [SerializeField] private Image abilityPfp;

    [Header("Ability logic???")]
    [SerializeField] private GameObject testAbiltyTirgger;

    // Public Getters (Read-Only)
    public float Damage => damage;
    public float CooldownTime => cooldownTime;

    public bool CanBurn => canBurn;
    public bool CanStun => canStun;
    public bool CanBleed => canBleed;
    public bool CanSlow => canSlow;

    public Image AbilityPfp => abilityPfp;

    public GameObject TestAbiltyTrigger => testAbiltyTirgger;
    public void ActivateAbility()
    {
        testAbiltyTirgger.GetComponent<IAbilities>().ActivateAbility();
    }
}
