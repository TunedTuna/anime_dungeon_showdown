using TMPro;
using UnityEngine;

public class LeverSwitchHandler : MonoBehaviour
{
    [SerializeField] private GameObject leverObj;
    [SerializeField] private bool isLeverOn = false;
    [SerializeField] private LeverSwitchSO leverSwitchSO;

    [Header("InteractStuff")]
    private bool playerInRange = false;
    public GameObject notifcationPanel;
    [SerializeField] private TextMeshProUGUI panelText;
    private const string leverText = "Press 'E' to flip lever";

    void Start()
    {
        PlayerInputController.OnInteract += PlayerInputController_OnInteract;
        NotificationPanelUI temp = ParentUI.Instance.PassRefDataNotificationUI();
        notifcationPanel = temp.GetPanelGameObject();
        panelText = temp.GetTextMeshProUGUIObject();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            notifcationPanel.SetActive(true);
            panelText.text = leverText;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideText();
            playerInRange = false;
        }
    }

    private void HideText()
    {
        notifcationPanel.SetActive(false);
    }

    private void ToggleLever()
    {
        isLeverOn = !isLeverOn;
        if (isLeverOn)
        {
            leverObj.transform.localEulerAngles = new Vector3(45f, transform.localEulerAngles.y, transform.localEulerAngles.z);
            GlobalEventManager.Fire_OnToggleLever(leverSwitchSO);
        }
        else
        {
            leverObj.transform.localEulerAngles = new Vector3(-45f, transform.localEulerAngles.y, transform.localEulerAngles.z);
            GlobalEventManager.Fire_OnToggleLever(leverSwitchSO);
        }

        //fire a lever toggled event. one event, but the listener SHOULD toggle the bool
    }

    private void PlayerInputController_OnInteract()
    {
        if (playerInRange)
        {

            ToggleLever();
        }
    }
    private void OnDisable()
    {
        PlayerInputController.OnInteract -= PlayerInputController_OnInteract;
    }
}
