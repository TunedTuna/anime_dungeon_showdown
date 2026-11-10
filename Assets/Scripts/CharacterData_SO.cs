using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class CharacterData_SO : ScriptableObject
{
    //todo make a "behavior script to "collab" with this
    [SerializeField] private string characterName;
    [SerializeField] private CharacterRole_Enum characterRole; //melee vs ranged

    [Header("Base Attack")]
    [SerializeField] private float baseDamage; 
    [SerializeField] private float currentDamage; //base +weapon+ effects(maybe)
    [SerializeField] private float attackRate;
    [SerializeField] private float attackTimer;
    [SerializeField] private float knockbackStrength;

    [Header("Health")]
    [SerializeField] private float currentHp;
    [SerializeField] private float maxHp;

    [Header("Movement")]
    [SerializeField] public float maxSpeed = 10f;
    [SerializeField] public float acceleration = 20f;
    [SerializeField] public float airControl = 0.5f;
    [SerializeField] private float baseSpeed;

    [Header("Dash")]
    [SerializeField] public float dashSpeed = 40f;
    [SerializeField] public float dashDuration = 0.2f;
    [SerializeField] public float dashCooldown = 1f;

    [Header("Gravity & Jump")]
    [SerializeField] public float gravity = -20f;
    [SerializeField] public float jumpHeight = 2f;

    [Header("Animation")]
    [SerializeField] public Animator animator;
    [SerializeField] private Sprite rosterSelectPfp; // for character select portrait
    [SerializeField] private Sprite spritePreview; // for that middle guy
    //getters

    [Header("Abilites")]
    [SerializeField] private Abilities_SO ability_1;
    [SerializeField] private Abilities_SO ability_2;
    [SerializeField] private Abilities_SO ability_3;

    [Header("RangedStuff")]
    [SerializeField] private int maxClip;
    
    public string CharacterName => characterName;
    public CharacterRole_Enum CharacterRole => characterRole;

    [Header("Base Attack")]
    public float BaseDamage => baseDamage;
    public float CurrentDamage => currentDamage;
    public float AttackRate => attackRate;
    public float AttackTimer => attackTimer;
    public float KnockbackStrength => knockbackStrength;

    [Header("Health")]
    public float CurrentHp => currentHp;
    public float MaxHp => maxHp;

    [Header("Movement")]
    public float MaxSpeed => maxSpeed;
    public float Acceleration => acceleration;
    public float AirControl => airControl;
    public float BaseSpeed => baseSpeed;

    [Header("Dash")]
    public float DashSpeed => dashSpeed;
    public float DashDuration => dashDuration;
    public float DashCooldown => dashCooldown;

    [Header("Gravity & Jump")]
    public float Gravity => gravity;
    public float JumpHeight => jumpHeight;

    [Header("Animation")]
    public Animator Animator => animator;
    public Sprite RosterSelectPfp => rosterSelectPfp;
    public Sprite SpritePreview => spritePreview;

    // Public Getters (Read-Only)
    public Abilities_SO Ability_1 => ability_1;
    public Abilities_SO Ability_2 => ability_2;
    public Abilities_SO Ability_3 => ability_3;

    [Header("RangedStuff")]
    public int MaxClip=> maxClip;



    //[Header("Specials")]
    //[SerializeField]
}
