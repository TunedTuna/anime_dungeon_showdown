using UnityEngine;
/// <summary>
/// helper to CHaracterSwap 
/// this script spawns the game object, uses Character Swap to decorate, then character swap to follow cammera
/// this could live outside game manager??
/// </summary>
public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ChA_Obj;
    [SerializeField] private GameObject ChB_Obj;
    [SerializeField] private Transform Spot_1;
    [SerializeField] private Transform Spot_2;
    private void Awake()
    {
        SpawnObjects();
        Debug.Log("SpawningCHaracters??");
    }
    private void SpawnObjects()
    {
        
        
        GameObject gameObject_1 =  Instantiate(ChA_Obj, Spot_1);
        GameObject gameObject_2 = Instantiate(ChB_Obj, Spot_2);

        GlobalEventManager.Fire_OnLevelLoaded(gameObject_1, gameObject_2);


    }

}
