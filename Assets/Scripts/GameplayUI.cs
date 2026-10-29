using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance { get; private set; }
    public AbilityUI ability1;
    public AbilityUI ability2;
    public AbilityUI ability3;
    public AbilityUI ability4;

    [Header("Ability data references")]
    [SerializeField] private Abilities_SO abilitySO_1;
    [SerializeField] private Abilities_SO abilitySO_2;
    [SerializeField] private Abilities_SO abilitySO_3;
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
    //todo: remove this update and replace with an event system. one for on use and another for cooldown ready to play their respective sounds
        if (Input.GetKeyDown(KeyCode.Q) && ability1.IsReady())
            ability1.UseAbility();
        if (Input.GetKeyDown(KeyCode.R) && ability2.IsReady())
            ability2.UseAbility();
        if (Input.GetKeyDown(KeyCode.T) && ability3.IsReady())
            ability3.UseAbility();
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }
}