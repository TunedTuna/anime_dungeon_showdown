using System;
using UnityEngine;
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

    //input swap
    public static event Action OnGoingToInput_UI;
    public static event Action OnGoingToInput_Player;

    //game states
    public static event Action OnGameOver;

    //deaths??
    public static event Action<GameObject> OnCharacterHasDied;


   

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
}
