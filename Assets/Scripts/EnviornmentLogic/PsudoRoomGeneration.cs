using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
/// <summary>
/// for this example, all tiles will be used
/// so, any unmarked rooms will be defaulted to "wild cards"
/// </summary>
public class PsudoRoomGeneration : MonoBehaviour
{
    [Header("map size data")]
    [SerializeField] private int maxMapY;
    [SerializeField] private int maxMapX;
    [SerializeField] private int totalMapSize;
    [SerializeField] private MapSizeEnum mapSize;

    [Header("map corners")]
    //[SerializeField] private int edgeZones;
    //[SerializeField] private int innerFlesh;
    //[SerializeField] private int cornerTracker;
    [SerializeField] private int nwIndex = 0; //always 0, so why  calc??
    [SerializeField] private int neIndex;
    [SerializeField] private int swIndex;
    [SerializeField] private int seIndex;

    [Header("RoomWorldSizeData")]
    [SerializeField] private int roomX = 32;
    [SerializeField] private int roomZ = 32;

    [Header("RoomRef??")]
    [SerializeField] private GameObject room;

    [Header("Map data")]
    [SerializeField] private GameObject[] mapListArray;
    [SerializeField] private List<int> pathArray;  //list for the dynamics
    [SerializeField] private int currentPathLocation;
    //[SerializeField] private List<Vector3> mapSpawnCoordList = new(); //for now, this will not be used
    //[SerializeField] private List<GameObject> roomPrefabs = new List<GameObject>(); // ditto above
    
    [SerializeField] private int minReqRooms = 2;

    [Header("PathStuff")]
    [SerializeField] private int roomTokens;
    [SerializeField] private int ogRoomTokens;

    [Header("Map Test data")]
    [SerializeField] private List<Vector3> testCoords = new();

    private void Start()
    {
        SetMapSize();
    }
    private void SetMapSize()
    {
        switch (mapSize)//could be cleaner but whateve's
        {
            case MapSizeEnum.Small:
                //
                maxMapX = (int)MapSizeEnum.Small;
                maxMapY = (int)MapSizeEnum.Small;
                totalMapSize = (maxMapX * maxMapY);
                break;
            case MapSizeEnum.Medium:
                //
                maxMapX = (int)MapSizeEnum.Medium;
                maxMapY = (int)MapSizeEnum.Medium;
                totalMapSize = (maxMapX * maxMapY);
                break;
            case MapSizeEnum.Large:
                maxMapX = (int)MapSizeEnum.Large;
                maxMapY = (int)MapSizeEnum.Large;
                totalMapSize = (maxMapX * maxMapY);
                break;
            default:
                break;

        }
        roomTokens = UnityEngine.Random.Range(minReqRooms, totalMapSize + 1);
        mapListArray = new GameObject[totalMapSize];
        Debug.Log("going to enter: generate room coords  ");
        GenerateRoomSpawnCoords();
        Debug.Log("going to enter: path labeling");
        PassOne_PathLabeling();
        PassTwo_Pathing();

    }
    private void GenerateRoomSpawnCoords()
    {

        //these would have to match map size... this is gonna get ugly. esp when they don't match
        //e.g. Z=4, x=3
        int wtf = 0;
        for (int i = 0; i < maxMapY; i++)
        {
            int tempRoomZ = roomZ * i;
            /* Z - 
             * Z -
             * Z -
             * Z -
             */
            for (int j = 0; j < maxMapX; j++)
            {
                //-
                //x,x,x,x,x
                //-

                //spawn row 0,0 at: (roomX * j) , sadY, roomZ
                int tempRoomX = roomX * j;
                Vector3 spawnPOS = new Vector3(tempRoomX, 0, tempRoomZ);
                testCoords.Add(spawnPOS);
                GameObject temp = Instantiate(room, spawnPOS, Quaternion.identity);
                mapListArray[wtf] = temp;
                wtf++;

            }

        }


    }
    private void PassOne_PathLabeling()
    {
        //is corner,edge,center
        //we know what the corners are, just hop through the loop :0
        //--always 4 corners, feed 'raw' int and the arrayCorner uses it to calc "corner positon"--
        ArrayCornerHelper();//north west corner


        //edges
        ArrayEdgeHelper();


    }
    private void ArrayCornerHelper()
    {
        //e.g. north east corner of a 3x3 would be: (3*[cornerNum]), (3*1)-1 = 2
        /* 0, 1, 2,
         * 3, 4, 5
         * 6, 7, 8
         */

        neIndex = maxMapX - 1;
        swIndex = maxMapX * (maxMapY - 1);
        seIndex = (maxMapX * maxMapY) - 1;
        //Debug.Log($"nw:{0}, ne:{maxMapX}")
        mapListArray[neIndex].GetComponent<RoomManager>().SetRoomLabel(RoomLabelEnum.Corner, CardinalDirectionEnum.North, CardinalDirectionEnum.East);
        mapListArray[nwIndex].GetComponent<RoomManager>().SetRoomLabel(RoomLabelEnum.Corner, CardinalDirectionEnum.North, CardinalDirectionEnum.West);
        mapListArray[seIndex].GetComponent<RoomManager>().SetRoomLabel(RoomLabelEnum.Corner, CardinalDirectionEnum.South, CardinalDirectionEnum.East);
        mapListArray[swIndex].GetComponent<RoomManager>().SetRoomLabel(RoomLabelEnum.Corner, CardinalDirectionEnum.South, CardinalDirectionEnum.East);
    }
    private void ArrayEdgeHelper()
    {
        //edges notth and south
        for (int i = 1; i < maxMapX - 1; i++)
        {
            Debug.Log(i);
            mapListArray[i].GetComponent<RoomManager>().SetRoomLabel(RoomLabelEnum.Edge, CardinalDirectionEnum.North, CardinalDirectionEnum.Any);
            int southIndex = swIndex + i;

            mapListArray[southIndex].GetComponent<RoomManager>().SetRoomLabel(RoomLabelEnum.Edge, CardinalDirectionEnum.South, CardinalDirectionEnum.Any);

        }
        //edges east and west
        for (int row = 1; row < maxMapY - 1; row++)
        {
            int westIndex = row * maxMapX;
            int eastIndex = (row * maxMapX) + (maxMapX - 1);

            mapListArray[westIndex].GetComponent<RoomManager>().SetRoomLabel(RoomLabelEnum.Edge, CardinalDirectionEnum.West, CardinalDirectionEnum.Any);
            mapListArray[eastIndex].GetComponent<RoomManager>().SetRoomLabel(RoomLabelEnum.Edge, CardinalDirectionEnum.East, CardinalDirectionEnum.Any);
        }

    }
    private void PassTwo_Pathing()
    {
        //use/exhaust tokens to create a path
        roomTokens = UnityEngine.Random.Range(minReqRooms, totalMapSize);
        ogRoomTokens = roomTokens;
        int startLocation = UnityEngine.Random.Range(0, totalMapSize);

        pathArray.Add(startLocation);
        currentPathLocation = startLocation;
        roomTokens--;
        while (roomTokens > 0)
        {
            //check all options
            DirectionFilter(currentPathLocation);
        }

    }
    private void DirectionFilter(int currentLocation)
    {
        List<int> cardinalDirections = new List<int> { 0, 1, 2, 3 };
        //
        //check label- label determines path:
        /* [0]    n
         * [1] w     e [2]
         * [3]    s
         */
        PathHelper(cardinalDirections, mapListArray[currentLocation].GetComponent<RoomManager>().GetCardinalDIrectionEnum());
        PathHelper(cardinalDirections, mapListArray[currentLocation].GetComponent<RoomManager>().GetCardinalDIrectionEnum());
        ChooseRemainingPathRandomly(cardinalDirections);

    }
    private void PathHelper(List<int> temp, CardinalDirectionEnum direction)
    {
        switch (direction)
        {
            case CardinalDirectionEnum.North:
                temp.Remove(0);
                break;
            case CardinalDirectionEnum.West:
                temp.Remove(1);
                break;
            case CardinalDirectionEnum.East:
                temp.Remove(2);
                break;
            case CardinalDirectionEnum.South:
                temp.Remove(3);
                break;
            default:
                //aka any

                break;
        }
    }
    private void ChooseRemainingPathRandomly(List<int> remainingList)
    {
        int rngNum = UnityEngine.Random.Range(0, remainingList.Count);
        mapListArray[currentPathLocation].GetComponent<RoomManager>().OpenDoorWay(rngNum);
        switch (remainingList[rngNum])
        {
            case 0:
                Go_North();
                break;
            case 1:
                Go_West();
                break;
            case 2:
                Go_East();
                break;
            case 3:
                Go_South();
                break;
        }
    }
    private void Go_North()
    {
        currentPathLocation -= maxMapX;
        AttemptRedeemRoomToken();

    }
    private void Go_South()
    {
        currentPathLocation += maxMapX;
        AttemptRedeemRoomToken();

    }
    private void Go_West()
    {
        currentPathLocation -= 1;
        AttemptRedeemRoomToken();
    }
    private void Go_East()
    {
        currentPathLocation += 1;
        AttemptRedeemRoomToken();
    }
    private void AttemptRedeemRoomToken()
    {
        if (!pathArray.Contains(currentPathLocation))
        {
            pathArray.Add(currentPathLocation);
            roomTokens--;
        }
    }
    private void PassThree_PathingNeighbors()
    {
        //check neighbors to narrow down path type: line, dead end, elbow, cross
    }
    private void PassFour_GenerateRooms()
    {

    }
}//end of PsudoROomCLass

public enum MapSizeEnum
{
    Small = 3,
    Medium = 4,
    Large = 5
}
