using TMPro;
using UnityEngine;

public class DoorStuff : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject notifcationPanel;
    [SerializeField] private TextMeshProUGUI panelText;
    private string doorText = "Press E to open Door";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerInputController.OnInteract += PlayerInputController_OnInteract;
        NotificationPanelUI temp = ParentUI.Instance.PassRefDataNotificationUI();
        notifcationPanel = temp.GetPanelGameObject();
        panelText = temp.GetTextMeshProUGUIObject();
    }

    private void PlayerInputController_OnInteract()
    {
        if (playerInRange)
        {
            HideText();
            playerInRange = false;
            UseTheDoor();

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            notifcationPanel.SetActive(true);
            //Debug.Log("Press E to open door");
            panelText.text = doorText;
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



    private void UseTheDoor()
    {
        //just destroy for simplicity. animate later

        Destroy(transform.parent.gameObject);
    }
    private void OnDisable()
    {
        PlayerInputController.OnInteract -= PlayerInputController_OnInteract;
    }

}
