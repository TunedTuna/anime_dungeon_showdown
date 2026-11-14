using System;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// honstly, idk what would go in here. everything but input??
/// </summary>
public static class GlobalEventManager 
{
    //--misc list off top of my head---
    //input swap helper
    //player controller input
    //ui controller input
    //enemyDied??
    //rosterslot decortation
    //abilty manager
    //------------------------------------------------

    //input swap this is just used to en
    public static event Action OnGoingToInput_UI;
    public static event Action OnGoingToInput_Player;

    //game states
    public static event Action OnGameOver;

    //deaths??
    public static event Action<GameObject> OnCharacterHasDied;

    //character slect conrifm
    public static event Action OnTeamFilled;

    //spawn characters in new level?
    public static event Action<(GameObject, GameObject)> OnLevelLoaded;

    public static event Action<LeverSwitchSO> OnToggleLever;

    //keys!
    public static event Action OnKeyPickedUp;
    public static event Action OnKeyUsed;

    public static event Action<float> OnHealActiveCharacter;

    //ui 
    public static event Action<bool> OnToggleAmmoUI;
    public static event Action<int> OnRangedCharacterDecorateUI;
    public static event Action<int> OnRangedCharacterFired;
    public static event Action OnRangedCharacterReload;

    //visuals
    public static event Action<ItemSO> OnItemPickedUp;
    public static void Fire_GoingToInput_UI()
    {
        OnGoingToInput_UI?.Invoke();
    }

    public static void Fire_GoingToInput_Player()
    {
        OnGoingToInput_Player?.Invoke();
    }
    public static void Fire_GameOver()
    {
        OnGameOver?.Invoke();
    }
    public static void Fire_OnCharacterHasDied(GameObject Character)
    {
        OnCharacterHasDied?.Invoke(Character);
    }
    public static void Fire_OnTeamFilled()
    {
        OnTeamFilled?.Invoke();
    }
    public static void Fire_OnLevelLoaded(GameObject Character, GameObject Character2)
    {
        (GameObject, GameObject) cheese= (Character, Character2);
        OnLevelLoaded?.Invoke(cheese);
    }
    public static void Fire_OnToggleLever(LeverSwitchSO incomingLeverResponse)
    {
        OnToggleLever?.Invoke(incomingLeverResponse);
    }
    public static void Fire_OnKeyPickedUp()
    {
        OnKeyPickedUp?.Invoke();
    }
    public static void Fire_OnKeyUsed()
    {
        OnKeyUsed?.Invoke();
    }
    public static void Fire_OnHealActiveCharacter(float hpToBeRestored)
    {
        OnHealActiveCharacter?.Invoke(hpToBeRestored);
    }
    public static void Fire_OnToggleAmmoUI(bool temp)
    {
        OnToggleAmmoUI?.Invoke(temp);
    }
    public static void Fire_OnRangedCharacterFired(int currentClip)
    {
        OnRangedCharacterFired?.Invoke(currentClip);
    }
    public static void Fire_OnRangedCharacterReload()
    {
        OnRangedCharacterReload?.Invoke();
    }
    public static void Fire_OnRangedCharacterDecorateUI(int maxClip)
    {
        OnRangedCharacterDecorateUI?.Invoke(maxClip);
    }

    public static void Fire_OnItemPickedUp(ItemSO obj)
    {
        OnItemPickedUp?.Invoke(obj);
        //send a scriptableObject to have other systems filter from it??
        //
    }
}
