using UnityEngine;

public class ParentUI : MonoBehaviour
{
    public static ParentUI Instance { get; private set; }

    [SerializeField] private CharacterSelectUI characterSelectUI;
    [SerializeField] private NotificationPanelUI notificationPanelUI;
    [SerializeField] private bool isUIOpen = false;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UserInterfaceInputController.Instance.OnCancel += UserInterfaceInput_OnCancel;
        UserInterfaceInputController.Instance.OnCloseAll += UserInterfaceInput_OnCloseAll;
        DisableChildren();
    }

    private void UserInterfaceInput_OnCloseAll(object sender, System.EventArgs e)
    {
        Debug.Log("InsideParent UI. closing all!");
        CallPlayerToUI();
       DisableChildren();
    }

    private void UserInterfaceInput_OnCancel(object sender, System.EventArgs e)
    {
       
    }
    //idk---
    public void CallPlayerToUI()
    {
        InputSwapHandler.Instance.EnablePlayerController();
    }
    public void CallUItoPlayer()
    {
        InputSwapHandler.Instance.EnableUserInterfaceInput();
    }

    public void ActivateCharacterSelectUI()
    {
        CallUItoPlayer();
        notificationPanelUI.HideUI();
        characterSelectUI.ShowUI();
    }
    //--Notification UI---
    public void ActivateNotificationUI()
    {
        CallUItoPlayer();
        notificationPanelUI.ShowUI();
    }
    public NotificationPanelUI PassRefDataNotificationUI()
    {
        return notificationPanelUI;
    }
    //what----
    private void DisableChildren()
    {
        //make these a Interface and we can easily loop through an array of these guys
        characterSelectUI.HideUI();
        notificationPanelUI.HideUI();  
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
        UserInterfaceInputController.Instance.OnCancel -= UserInterfaceInput_OnCancel;
        UserInterfaceInputController.Instance.OnCloseAll -= UserInterfaceInput_OnCloseAll;
    }
}
