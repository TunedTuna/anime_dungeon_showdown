using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorStuff : MonoBehaviour
{
    //door stuff
    [SerializeField] private DoorOpenMethod dom; //open "animation
    [SerializeField] private DoorTypeEnum doorType; //open, lock'n'key, gate'n'levers
    [SerializeField] private DoorLeverType leverType;
    [SerializeField] private GameObject theDoor;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Material transparentMat;

    [SerializeField] private List<ActivatorInfoClass> activatorList= new(); // hold a list of scriptable objects
    [SerializeField] private bool isOpened = false;
    [SerializeField] private bool isLocked = false;
    [SerializeField] private Key_SO keyItem;
    [SerializeField] private DoorTargetSO doorID;

    [Header("DoorType?")]
    [SerializeField] private GameObject doorLockObj;
    [SerializeField] private GameObject doorGateObj;

    //patternLock
    [Header("Patterns for Switches")]
    [SerializeField] private List<GameObject> patternAnswerCombo;
    [SerializeField] private List<GameObject> currentPatternCombo;

    //interact stuff
    [Header("InteractStuff")]
    private bool playerInRange = false;
    public GameObject notifcationPanel;
    [SerializeField] private TextMeshProUGUI panelText;
    
    private const string unlockedDoorText = "Press E to open Door";
    private const string lockedDoorText = "Press E to unlock Door";
    [SerializeField] private Inventory playerInv;

    void Start()
    {
        PlayerInputController.OnInteract += PlayerInputController_OnInteract;
        NotificationPanelUI temp = ParentUI.Instance.PassRefDataNotificationUI();
        notifcationPanel = temp.GetPanelGameObject();
        panelText = temp.GetTextMeshProUGUIObject();
        boxCollider=GetComponent<BoxCollider>();
        DoorAesthetic();
    }

    private void PlayerInputController_OnInteract()
    {
        if (playerInRange)
        {
            if (isLocked)
            {
                LockedDoorCheck();
                
            }
            else
            {
                HideText();
                playerInRange = false;
                UseTheDoor();
            }


        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            notifcationPanel.SetActive(true);
            //Debug.Log("Press E to open door");
            if(isLocked)
            {
                panelText.text = lockedDoorText;
            }
            else
            {
                panelText.text = unlockedDoorText;

            }
            playerInv= other.GetComponent<Inventory>();
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
        if (!isOpened && !isLocked)
        {
            boxCollider.enabled = false;
            isOpened = true;
            PrototypeOpenAnimation();
            theDoor.GetComponent<MeshRenderer>().material = transparentMat;
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
    private void DoorAesthetic()
    {
        switch (doorType)
        {
            case DoorTypeEnum.Gated:
                //
                doorGateObj.SetActive(true);
                boxCollider.enabled = false;
                break;
            case DoorTypeEnum.Locked:
                //
                panelText.text = "Press 'E' to unlock door";
                doorLockObj.SetActive(true);
                isLocked = true;
                break;
            default:
                break;

        }
    }
    private void RemoveGate()
    {
        doorGateObj.SetActive(false);
        boxCollider.enabled = true;
        //this has to come from an event
    }
    private void RemoveLock()
    {
        doorLockObj.SetActive(false);
        isLocked = false;
        panelText.text = unlockedDoorText;
    }

    private void LockedDoorCheck()
    {
        //check if key item is in player inventory 
        for(int i=0; i<playerInv.GetKeys().Count; i++)
        {
            if (playerInv.GetKeys()[i] == keyItem)
            {
                playerInv.GetKeys().RemoveAt(i);
                RemoveLock();
                return;
            }
            else
            {
                //notify player? noise, jingle animation?
            }
        }


    }
    public void LeverResponse(LeverSwitchSO leverResponse)
    {
        switch (leverType)
        {
            case DoorLeverType.Simple:
                //
                SimpleSwitchCheck(leverResponse);
                break;
            case DoorLeverType.Pattern:
                //break
                break;
            default:
                break;
        }
    }
    private void PatternSwitchCheck(LeverSwitchSO temp)
    {
        //call everytime a switch is hit
        //compare current to answer key

    }
    private void SimpleSwitchCheck(LeverSwitchSO temp)
    {
        int numOfActivatorOn= activatorList.Count;
        //check if every switch that is referenced is true
        for(int i=0; i < activatorList.Count; i++)
        {
            if(temp== activatorList[i].activatorData)
            {
                activatorList[i].isActive = true;
                
            }
            if(activatorList[i].isActive)
            {
                numOfActivatorOn--;
            }
        }

        if (numOfActivatorOn == 0)
        {
            RemoveGate();
        }


    }

    public DoorTargetSO GetTargetDoorSO()
    {
        return doorID;
    }
}
public enum DoorOpenMethod
{
    Lift,
    Swing,
    Slide,
    Fall,
}
public enum DoorLeverType
{
    Simple,
    Pattern
}

[System.Serializable]
public class ActivatorInfoClass
{
    public LeverSwitchSO activatorData;
    public bool isActive =false; //default false
}


