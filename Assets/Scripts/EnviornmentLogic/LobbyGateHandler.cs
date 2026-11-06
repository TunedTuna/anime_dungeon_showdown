using UnityEngine;
/// <summary>
/// because this is specifc and only gonna happen once, do it like this...?
/// 
/// </summary>
public class LobbyGateHandler : MonoBehaviour
{
    [SerializeField] private GameObject GateObject; 
    [SerializeField] private CharacterSwap characterSwap;
    private void OnEnable()
    {
        GlobalEventManager.OnTeamFilled += GlobalEventManager_OnTeamFilled;
        characterSwap= CharacterSwap.instance;//this is cooked if race conditon...
        if(characterSwap!=null)
        {
            if(characterSwap.GetSlotData_A()!=null && characterSwap.GetSlotData_A() != null)
            {
                JimmyTheDoor();
            }
        }
    }

    private void GlobalEventManager_OnTeamFilled()
    {
        JimmyTheDoor();
    }

    private void OnDisable()
    {
        GlobalEventManager.OnTeamFilled -= GlobalEventManager_OnTeamFilled;
    }
    private void JimmyTheDoor()
    {
        GateObject.GetComponent<DoorStuff>().ForceTheDoorOpen();
    }
}
