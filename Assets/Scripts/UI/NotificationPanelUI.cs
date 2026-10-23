using TMPro;
using UnityEngine;

public class NotificationPanelUI : MonoBehaviour
{

    public static NotificationPanelUI Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI notificationText;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public GameObject GetPanelGameObject()
    {
        return gameObject;
    }
    public TextMeshProUGUI GetTextMeshProUGUIObject()
    {
        return notificationText;
    }
       
    
}
