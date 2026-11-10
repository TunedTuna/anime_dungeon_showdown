using TMPro;
using UnityEngine;
//too lazy to do the raycast :/
public class CharacterSelectHandler : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private bool playerInRange = false;
    public GameObject notifcationPanel;
    [SerializeField] private TextMeshProUGUI panelText;
    NotificationPanelUI temp;
    private string openCharacterSelectText = "Hold E to Select your characters!";
    //
    void Start()
    {
        PlayerInputController.OnInteract += PlayerInputController_OnInteract;

        temp = ParentUI.Instance.PassRefDataNotificationUI();
        notifcationPanel = temp.GetPanelGameObject();
        panelText = temp.GetTextMeshProUGUIObject();
    }

    private void PlayerInputController_OnInteract()
    {
        if (playerInRange)
        {
            //CharacterSelectUI.Instance.ShowUI();
            //todo replace w/ parent handler
            ParentUI.Instance.ActivateCharacterSelectUI();

            InputSwapHandler.Instance.EnableUserInterfaceInput();

        }
    }

 

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag(PLAYER_TAG)))
        {
            playerInRange = true;
            temp.ShowUI();
            panelText.text = openCharacterSelectText;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag(PLAYER_TAG)))
        {
            playerInRange = false;
            temp.HideUI();  
            panelText.text = "Empty";
        }
    }
    private void OnDisable()
    {
        PlayerInputController.OnInteract -= PlayerInputController_OnInteract;
    }



}
