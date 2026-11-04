using UnityEngine;

public class ParentUI : MonoBehaviour
{
    public static ParentUI Instance { get; private set; }

    [Header("GamePlayUI")]
    [SerializeField] private GameplayUI hud;

    [Header("UI...UI? Menus")]
    [SerializeField] private CharacterSelectUI characterSelectUI;
    [SerializeField] private NotificationPanelUI notificationPanelUI;
    [SerializeField] private PauseUI pauseUI;

    [SerializeField] private bool isUIopen;
    [SerializeField] private bool isGameOver;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UserInterfaceInputController.Instance.OnCancel += UserInterfaceInput_OnCancel;
        UserInterfaceInputController.Instance.OnCloseAll += UserInterfaceInput_OnCloseAllMenus;
        DisableChildren_Menus();
    }
    private void OnEnable()
    {
        GlobalEventManager.OnGoingToInput_Player += GlobalEventManager_OnGoingToInput_Player;
        GlobalEventManager.OnGoingToInput_UI += GlobalEventManager_OnGoingToInput_UI;
        GlobalEventManager.OnGameOver += GlobalEventManager_OnGameOver;
    }

    private void GlobalEventManager_OnGameOver()
    {
        isGameOver = true;
        
    }

    private void GlobalEventManager_OnGoingToInput_UI()
    {
        //when uiOpens
        isUIopen = true;

    }

    private void GlobalEventManager_OnGoingToInput_Player()
    {
        isUIopen = false;
        hud.ShowUI();
    }

    private void UserInterfaceInput_OnCloseAllMenus(object sender, System.EventArgs e)
    {
        //this is the 'esc' key btw
        if (!isGameOver)
        {

            if (isUIopen)
            {
                isUIopen = false;
                Debug.Log("InsideParent UI. closing all!");
                CallUItoPlayer();
                DisableChildren_Menus();

            }
            else
            {
                isUIopen = true;
                Debug.Log("showUI pause");
                pauseUI.ShowUI();
            }
        }
    }

    private void UserInterfaceInput_OnCancel(object sender, System.EventArgs e)
    {
        if (isUIopen)
        {

        }

    }
    //idk--- these "calls" kinda redundat but what evs
    public void CallPlayerToUI()
    {

        GlobalEventManager.Fire_GoingToInput_UI();
    }
    public void CallUItoPlayer()
    {

        GlobalEventManager.Fire_GoingToInput_Player();
    }

    public void ActivateCharacterSelectUI()
    {
        GlobalEventManager.Fire_GoingToInput_UI();
        CallPlayerToUI();
        notificationPanelUI.HideUI();
        characterSelectUI.ShowUI();
    }
    //--Notification UI---
    public void ActivateNotificationUI()
    {
        CallPlayerToUI();
        notificationPanelUI.ShowUI();
    }
    public NotificationPanelUI PassRefDataNotificationUI()
    {
        return notificationPanelUI;
    }
    //what----
    private void DisableChildren_Menus()
    {
        //make these a Interface and we can easily loop through an array of these guys
        characterSelectUI.HideUI();
        notificationPanelUI.HideUI();
        pauseUI.HideUI();
    }


    private void OnDisable()
    {
        //this script is only really disabled when it is destroyed...so...
        UserInterfaceInputController.Instance.OnCancel -= UserInterfaceInput_OnCancel;
        UserInterfaceInputController.Instance.OnCloseAll -= UserInterfaceInput_OnCloseAllMenus;

        GlobalEventManager.OnGoingToInput_Player -= GlobalEventManager_OnGoingToInput_Player;
        GlobalEventManager.OnGoingToInput_UI -= GlobalEventManager_OnGoingToInput_UI;
    }
}
