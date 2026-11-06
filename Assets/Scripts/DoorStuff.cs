using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorStuff : MonoBehaviour
{
    //door stuff
    [SerializeField] private DoorOpenMethod dom;
    [SerializeField] private GameObject theDoor;
    [SerializeField] private List<(GameObject switchData, bool isActive)> switchList; // hold a list of scriptable objects
    [SerializeField] private bool isOpened = false;
    [SerializeField] private bool isLocked = false;
    [SerializeField] private GameObject key; //idk what data type yet

    //patternLock
    [Header("Patterns for Switches")]
    [SerializeField] private List<GameObject> patternAnswerCombo;
    [SerializeField] private List<GameObject> currentPatternCombo;

    //interact stuff
    [Header("InteractStuff")]
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

        //Destroy(transform.parent.gameObject);
        if (!isOpened)
        {
            isOpened = true;
            PrototypeOpenAnimation();
        }
    }
    public void ForceTheDoorOpen()
    {
        PrototypeOpenAnimation();
    }
    private void OnDisable()
    {
        PlayerInputController.OnInteract -= PlayerInputController_OnInteract;
    }

    public void PrototypeOpenAnimation()
    {
        Transform temp = theDoor.transform;
        float temp_X = temp.position.x;
        float temp_Y = temp.position.y;
        float temp_Z = temp.position.z;
        switch (dom)
        {
            case DoorOpenMethod.Swing:
                //
                break;
            case DoorOpenMethod.Lift:
                //
                theDoor.transform.position = new Vector3(temp_X, temp_Y + 10f, temp_Z);
                break;
            case DoorOpenMethod.Slide:
                //
                break;
            case DoorOpenMethod.Fall:
                //
                break;

            default:
                //
                break;
        }

    }
   
    private void LockedDoorCheck()
    {
        //check if key item is in player inventory 
    }
    private void PatternSwitchCheck()
    {
        //call everytime a switch is hit
        //compare current to answer key

    }
    private void SimpleSwitchCheck()
    {
        //check if every switch that is referenced is true
    }

}
public enum DoorOpenMethod
{
    Lift,
    Swing,
    Slide,
    Fall,
}


