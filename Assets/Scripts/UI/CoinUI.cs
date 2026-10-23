using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public static CoinUI Instance {  get; private set; }
    [SerializeField] private TextMeshProUGUI coinTextObject;
    private void Awake()
    {
        Instance = this;
    }
    public TextMeshProUGUI GetCoinTextObject()
    {
        return coinTextObject;
    }

}
