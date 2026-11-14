using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    [Header("Shared Data (Written Once!)")]
    [SerializeField] private string itemName;
    [SerializeField] private Sprite icon;

    public string ItemName => itemName;
    public Sprite Icon => icon;

    // EVERY child class MUST provide its own unique logic for this
    public abstract void Use();

}
