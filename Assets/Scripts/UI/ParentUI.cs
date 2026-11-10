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
        PlayerInputController.OnPauseOpened += PlayerInputController_OnPauseOpened;
        
        DisableChildren_Menus();
    }

  

    private void PlayerInputController_OnPauseOpened()
    {
        Debug.Log("showUI pause");
        pauseUI.ShowUI();
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

    
                isUIopen = false;
                Debug.Log("InsideParent UI. closing all!");
                
                DisableChildren_Menus();

            
        }
    }

    private void UserInterfaceInput_OnCancel(object sender, System.EventArgs e)
    {


    }
    //idk--- these "calls" kinda redundat but what evs
    public void CallPlayerToUI()
    {
        InputSwapHandler.Instance.EnableUserInterfaceInput();
        
    }


    public void ActivateCharacterSelectUI()
    {
      
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
        PlayerInputController.OnPauseOpened -= PlayerInputController_OnPauseOpened;
    }
}
