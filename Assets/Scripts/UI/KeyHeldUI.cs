using TMPro;
using UnityEngine;

public class KeyHeldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textObj;
    [SerializeField] private int keyCount = 0; 
    

    private void OnEnable()
    {
        GlobalEventManager.OnKeyPickedUp += GlobalEventManager_OnKeyPickedUp;
        GlobalEventManager.OnKeyUsed += GlobalEventManager_OnKeyUsed;
        textObj.text = $"x{keyCount}";
    }

    private void GlobalEventManager_OnKeyUsed()
    {
        keyCount--;
        textObj.text= $"x{keyCount}";   
    }

    private void GlobalEventManager_OnKeyPickedUp()
    {
        keyCount++;
        textObj.text = $"x{keyCount}";
    }
}
