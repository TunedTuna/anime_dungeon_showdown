using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject Chest; //when spawn chest, feed it this map's loot table
    [SerializeField] private List<Transform> chestSpawnLocation = new List<Transform>();
    [SerializeField] private TestLootTableHandler lootTableHandler;
    //give a list of enemy so to feed into the EnemySpawner?

    [SerializeField] private List<DoorTargetSO> doorTargetList;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject deadCharacter;

    [Header("labels")]
    [SerializeField] private RoomLabelEnum roomLabelEnum;
    [SerializeField] private CardinalDirectionEnum cardinalDirection;
    [SerializeField] private CardinalDirectionEnum secondaryCardinalDirection;
    [SerializeField] private int keyThing;


    [Header("TestingDoor(s)")]
    [SerializeField] private List<GameObject> doorListObj;
    [SerializeField] private GameObject northDoorWay;
    [SerializeField] private GameObject westDoorWay;
    [SerializeField] private GameObject eastDoorWay;
    [SerializeField] private GameObject southDoorWay;

    private void OnEnable()
    {
        boxCollider = GetComponent<BoxCollider>();
        enemySpawner = GetComponent<EnemySpawner>();
        lootTableHandler = GetComponent<TestLootTableHandler>();
        GlobalEventManager.OnCharacterHasDied += GlobalEventManager_OnCharacterHasDied;
    }
    private void OnDisable()
    {
        GlobalEventManager.OnCharacterHasDied -= GlobalEventManager_OnCharacterHasDied;
    }

    private void GlobalEventManager_OnCharacterHasDied(GameObject obj)
    {
        deadCharacter = obj;
        //sub member swithc location is inconsistent
        //ReleaseTheGates();
        //boxCollider.enabled = true;
        //Debug.Log("character died, releasing gates!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SealTheExits();
            enemySpawner.BeginOnslaught();
            boxCollider.enabled = false;
        }
    }
    private void SealTheExits()
    {
        for (int i = 0; i < doorListObj.Count; i++)
        {
            doorListObj[i].GetComponent<DoorStuff>().CloseGate();
        }
    }
    public void ReleaseTheGates()
    {
        for (int i = 0; i < doorListObj.Count; i++)
        {
            doorListObj[i].GetComponent<DoorStuff>().RemoveGate();
        }
    }
    public void DoStuffAfterBattle()
    {
        ReleaseTheGates();
        SpawnSomeLoot();

    }
    private void SpawnSomeLoot()
    {
        int randNum = UnityEngine.Random.Range(0, chestSpawnLocation.Count);
        GameObject tempChest = Instantiate(Chest, chestSpawnLocation[randNum]);
        tempChest.GetComponent<DumbCHestMiddleManHandler>().GetChestScript().SetLoot(lootTableHandler.GetSomeLoot());


    }
    public void SetRoomLabel(RoomLabelEnum newLabel, CardinalDirectionEnum newDir, CardinalDirectionEnum secNewDir)
    {
        roomLabelEnum = newLabel;
        cardinalDirection = newDir;
        secondaryCardinalDirection = secNewDir;

    }
    public RoomLabelEnum GetRoomLabel()
    {
        return roomLabelEnum;
    }
    public CardinalDirectionEnum GetCardinalDIrectionEnum()
    {
        return cardinalDirection;
    }
    public CardinalDirectionEnum GetSecondaryCardinalDirection()
    {
        return secondaryCardinalDirection;
    }
    public int GetKeyThing()
    {
        return keyThing;
    }

    public void OpenDoorWay(int bird)
    {
        switch (bird)
        {
            case 0:
                southDoorWay.SetActive(false); break;
                
            case 1:
                westDoorWay.SetActive(false); break;
                
            case 2:
                westDoorWay.SetActive(false); break;
            case 3:
                northDoorWay.SetActive(false); break;
        }
    }
}
