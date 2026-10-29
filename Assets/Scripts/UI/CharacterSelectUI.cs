using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("LeftPart")]
    [SerializeField] private Image PfpSlotA;
    [SerializeField] private Image PfpSlotB;

    [Header("Middle Part")]
    [SerializeField] private TextMeshProUGUI characterNamePreview;
    [SerializeField] private Image characterSpritePreview;

    [Header("RightPart")]
    [SerializeField] private TextMeshProUGUI rolePreview;
    [SerializeField] private TextMeshProUGUI healthPreviewText;
    [SerializeField] private TextMeshProUGUI baseDamagePreviewText;
    [SerializeField] private TextMeshProUGUI baseSpeedPreview;

    [Header("RightPart-Part2: Da Barz")]
    [SerializeField] private Image hpScaleBar;
    [SerializeField] private Image atkScaleBar;
    [SerializeField] private Image spdScaleBar;

    [Header("TempCharacterData")]
    [SerializeField] private CharacterData_SO tempSlotA_SO; //is "temp" incase player quit's w/out "saving"
    [SerializeField] private CharacterData_SO tempSlotB_SO;
    [SerializeField] private ActiveSlotEnum activeSLot; //idk if this is GOING to do anything...
    [SerializeField] private bool isActiveA;

    [Header("TempMaxStatsData")]
    [SerializeField] private float globalMaxHp = 50;
    [SerializeField] private float globalMaxAtk = 50;
    [SerializeField] private float globalMaxSpd = 30;
    //ability ref- prob populate the data here, but have it handle it's own data display?
    //[SerializeField] private 
    public enum ActiveSlotEnum
    {
        slotA,
        slotB
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        HideUI();
        RosterSlotDecorator.OnUpdateCharacterSelectUI -= RosterSlotDecorator_OnUpdateCharacterSelectUI;
        UserInterfaceInputController.Instance.OnSwap -= UserInterfaceInputController_OnSwap;

    }
    private void OnEnable()
    {
        ShowUI();
        RosterSlotDecorator.OnUpdateCharacterSelectUI += RosterSlotDecorator_OnUpdateCharacterSelectUI;
        if (UserInterfaceInputController.Instance != null)//todo fix this race condition
        {
            UserInterfaceInputController.Instance.OnSwap += UserInterfaceInputController_OnSwap;
            UserInterfaceInputController.Instance.OnSubmit += UserInterfaceCOntroller_OnSubmit;

        }
        ClearDataOnScreen();
        DisplayCurrentParty();
        isActiveA = true;
        //listen to 'SwapKey' from UI controller


    }

    private void UserInterfaceCOntroller_OnSubmit(object sender, System.EventArgs e)
    {
        CharacterSwap.instance.RefreshCharacterData(tempSlotB_SO, tempSlotA_SO);
        InputSwapHandler.Instance.EnablePlayerController();
        HideUI();
    }

    private void UserInterfaceInputController_OnSwap(object sender, System.EventArgs e)
    {
        isActiveA = !isActiveA; //flip

    }

    private void RosterSlotDecorator_OnUpdateCharacterSelectUI(CharacterData_SO characterData_SO)
    {
        DecorateUIPreview(characterData_SO);
    }

    private void DecorateUIPreview(CharacterData_SO previewCh)
    {
        characterNamePreview.text = previewCh.CharacterName;
        characterSpritePreview.sprite = previewCh.SpritePreview;
        rolePreview.text = previewCh.CharacterRole.ToString();
        healthPreviewText.text = previewCh.MaxHp.ToString();
        baseDamagePreviewText.text = previewCh.BaseDamage.ToString();
        baseSpeedPreview.text = previewCh.BaseSpeed.ToString();
        //da barz!!!
        ChangeDaBarGraph(hpScaleBar, previewCh.MaxHp, globalMaxHp);
        ChangeDaBarGraph(atkScaleBar, previewCh.BaseDamage, globalMaxAtk);
        ChangeDaBarGraph(spdScaleBar, previewCh.BaseSpeed, globalMaxSpd);

        if (isActiveA)
        {
            if (previewCh == tempSlotB_SO)
            {//swap them
                tempSlotB_SO = SimpleTempSwap(PfpSlotB, tempSlotA_SO);

            }
            PfpSlotA.sprite = previewCh.RosterSelectPfp;
            tempSlotA_SO = previewCh;
        }
        else
        {
            if (previewCh == tempSlotA_SO)
            {
                tempSlotA_SO = SimpleTempSwap(PfpSlotA, tempSlotB_SO);
            }
            PfpSlotB.sprite = previewCh.RosterSelectPfp;
            tempSlotB_SO = previewCh;
        }


    }
    private CharacterData_SO SimpleTempSwap(Image tempImage, CharacterData_SO replacee)
    {
        //SO act funny if you try to edit within a method
        tempImage.sprite = replacee.RosterSelectPfp;
        return replacee;

    }

    private void ChangeDaBarGraph(Image edit, float tempy, float global)
    {
        edit.fillAmount = tempy / global;
    }
    private void ClearDataOnScreen()
    {
        ChangeDaBarGraph(hpScaleBar, 0, globalMaxHp);
        ChangeDaBarGraph(atkScaleBar, 0, globalMaxAtk);
        ChangeDaBarGraph(spdScaleBar, 0, globalMaxSpd);

        characterNamePreview.text = "-----";
        characterSpritePreview.sprite = null;
        rolePreview.text = "-----";
        healthPreviewText.text = "-";
        baseDamagePreviewText.text = "-";
        baseSpeedPreview.text = "-";

    }
    private void DisplayCurrentParty()
    {
        Debug.LogWarning($"race condition: CharacterSwapInstance ");
        if (CharacterSwap.instance.GetCurrentPart().Item1 != null && CharacterSwap.instance.GetCurrentPart().Item2 != null) //todo characterswap race conditon
        {
            (CharacterData_SO, CharacterData_SO) cheese = CharacterSwap.instance.GetCurrentPart();

            PfpSlotA.sprite = cheese.Item1.RosterSelectPfp;
            PfpSlotB.sprite = cheese.Item2.RosterSelectPfp;

        }
    }


}
