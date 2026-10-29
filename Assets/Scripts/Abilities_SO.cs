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

}
