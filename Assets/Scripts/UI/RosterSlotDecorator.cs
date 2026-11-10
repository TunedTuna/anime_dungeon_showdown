using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RosterSlotDecorator : MonoBehaviour
{
    [SerializeField] private CharacterData_SO characterData_SO;
    [SerializeField] private TextMeshProUGUI nameTextBox;
    [SerializeField] private Image pfpFrame;

    public delegate void UpdateCharacterSelectUI(CharacterData_SO characterData_SO);
    public static event UpdateCharacterSelectUI OnUpdateCharacterSelectUI;

    [SerializeField] private Button rosterBtm;
    public event EventHandler OnRosterSlotPreview;
    private void OnEnable()
    {
        nameTextBox.text = characterData_SO.CharacterName;
        pfpFrame.sprite = characterData_SO.RosterSelectPfp;
        rosterBtm.onClick.AddListener(() => { CharacterSelected(); });
    }
    private void OnDisable()
    {
        rosterBtm.onClick.RemoveListener(() => { CharacterSelected(); });
    }
    private void CharacterSelected()
    {
        OnUpdateCharacterSelectUI?.Invoke(characterData_SO);
    }
    


}
