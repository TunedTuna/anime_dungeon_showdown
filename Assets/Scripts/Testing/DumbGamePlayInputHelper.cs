using UnityEngine;

public class DumbGamePlayInputHelper : MonoBehaviour
{
    [SerializeField] private GameObject PlayerThingy;

    private void OnEnable()
    {
        GlobalEventManager.OnGoingToInput_Player += GlobalEventManager_OnGoingToInput_Player;
        GlobalEventManager.OnGoingToInput_UI += GlobalEventManager_OnGoingToInput_UI;
    }

    private void GlobalEventManager_OnGoingToInput_UI()
    {
        PlayerThingy.SetActive(false);
    }

    private void GlobalEventManager_OnGoingToInput_Player()
    {
        PlayerThingy.SetActive(true);
    }
    private void OnDisable()
    {
        GlobalEventManager.OnGoingToInput_Player -= GlobalEventManager_OnGoingToInput_Player;
        GlobalEventManager.OnGoingToInput_UI -= GlobalEventManager_OnGoingToInput_UI;
    }

}
