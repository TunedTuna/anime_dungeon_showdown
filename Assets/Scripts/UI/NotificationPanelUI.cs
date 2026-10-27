using TMPro;
using UnityEngine;
//there is one of these, but more than 1 thing will edit it...
public class NotificationPanelUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI notificationText;

    public GameObject GetPanelGameObject()
    {
        return gameObject;
    }
    public TextMeshProUGUI GetTextMeshProUGUIObject()
    {
        return notificationText;
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        HideUI();
    }

}
