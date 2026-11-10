using UnityEngine;

public class DumbGamePlayInputHelper : MonoBehaviour
{
    [SerializeField] private GameObject PlayerThingy;
    [SerializeField] private bool isCHildEnabled;

    private void OnEnable()
    {
        GlobalEventManager.OnGoingToInput_Player += GlobalEventManager_OnGoingToInput_Player;
        GlobalEventManager.OnGoingToInput_UI += GlobalEventManager_OnGoingToInput_UI;
        isCHildEnabled = PlayerThingy.activeInHierarchy;
    }

    private void GlobalEventManager_OnGoingToInput_UI()
    {
        PlayerThingy.SetActive(false);
        //Debug.Log("------------disabling player object inpiut");
        isCHildEnabled = PlayerThingy.activeInHierarchy;
    }

    private void GlobalEventManager_OnGoingToInput_Player()
    {
        PlayerThingy.SetActive(true);
        //Debug.Log("re enabling player object inpiut");
        isCHildEnabled = PlayerThingy.activeInHierarchy;
    }
    private void OnDisable()
    {
        GlobalEventManager.OnGoingToInput_Player -= GlobalEventManager_OnGoingToInput_Player;
        GlobalEventManager.OnGoingToInput_UI -= GlobalEventManager_OnGoingToInput_UI;
    }

}
