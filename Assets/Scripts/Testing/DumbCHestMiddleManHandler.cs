using UnityEngine;

public class DumbCHestMiddleManHandler : MonoBehaviour
{

    [SerializeField] private Chest chestScript;
    public Chest GetChestScript()
    {
        return chestScript;
    }
}
