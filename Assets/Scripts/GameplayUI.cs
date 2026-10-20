using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    public AbilityUI ability1;
    public AbilityUI ability2;
    public AbilityUI ability3;
    public AbilityUI ability4;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && ability1.IsReady())
            ability1.UseAbility();
        if (Input.GetKeyDown(KeyCode.R) && ability2.IsReady())
            ability2.UseAbility();
        if (Input.GetKeyDown(KeyCode.T) && ability3.IsReady())
            ability3.UseAbility();
    }
    //todo: remove this update and replace with an event system. one for on use and another for cooldown ready to play their respective sounds
}