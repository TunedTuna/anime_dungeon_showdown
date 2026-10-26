using TMPro;
using UnityEngine;
//too lazy to do the raycast :/
public class CharacterSelectHandler : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject notifcationPanel;
    [SerializeField] private TextMeshProUGUI panelText;
    private string doorText = "Press E to Select your characters!";
    //
    void Start()
    {
        PlayerInputController.instance.OnInteract += PlayerControllerInput_OnInteract;
        if (NotificationPanelUI.Instance != null)
        {
            notifcationPanel = NotificationPanelUI.Instance.GetPanelGameObject();
            panelText = NotificationPanelUI.Instance.GetTextMeshProUGUIObject();

        }
    }

    private void PlayerControllerInput_OnInteract(object sender, System.EventArgs e)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
