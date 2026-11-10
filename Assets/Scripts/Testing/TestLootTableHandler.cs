using UnityEngine;
using System.Collections.Generic;

public class TestLootTableHandler : MonoBehaviour
{
    [SerializeField] private List<ScriptableObject> lootList = new List<ScriptableObject>();

    public ScriptableObject GetSomeLoot()
    {
        int randNum = UnityEngine.Random.Range(0, lootList.Count);
        return lootList[randNum];
    }
}
