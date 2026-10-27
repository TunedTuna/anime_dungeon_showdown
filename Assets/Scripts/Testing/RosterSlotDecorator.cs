using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RosterSlotDecorator : MonoBehaviour
{
    [SerializeField] private CharacterData_SO characterData_SO;
    [SerializeField] private TextMeshProUGUI nameTextBox;
    [SerializeField] private Image pfpFrame;

    private void OnEnable()
    {
        nameTextBox.text = characterData_SO.CharacterName;
        pfpFrame.sprite = characterData_SO.RosterSelectPfp;
    }
}
