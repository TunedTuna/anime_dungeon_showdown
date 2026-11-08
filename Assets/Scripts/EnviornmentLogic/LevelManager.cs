using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<DoorStuff> doorList = new();

    private void Awake()
    {
        GlobalEventManager.OnToggleLever += GlobalEventManager_OnToggleLever;
    }

    private void GlobalEventManager_OnToggleLever(LeverSwitchSO temp)
    {
       for (int i = 0; i < doorList.Count; i++)
        {
            if (doorList[i].GetTargetDoorSO()==temp.DoorTarget)
            {
                doorList[i].LeverResponse(temp);
            }
        }
    }
}
