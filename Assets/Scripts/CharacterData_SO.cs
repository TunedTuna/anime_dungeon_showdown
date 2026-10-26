using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class CharacterData_SO : ScriptableObject
{
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
    [SerializeField] private Image pfp;

    //[Header("Specials")]
    //[SerializeField]
}
