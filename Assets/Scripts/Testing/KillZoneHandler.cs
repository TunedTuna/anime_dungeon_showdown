using UnityEngine;

public class KillZoneHandler : MonoBehaviour
{
    [SerializeField] private Transform playerRespawnPoint;
    private string PLAYER_TAG= "Player";
    private string ENEMY_TAG;
    

    private void OnTriggerEnter(Collider other)
    {
        GameObject temp = other.gameObject;
        bool movementScriptExists = temp.GetComponent<PlayerMovement>() != null;
        if (temp.CompareTag(PLAYER_TAG) && movementScriptExists )
        {

            temp.GetComponent<PlayerMovement>().WarpPlayer(playerRespawnPoint);
            Debug.Log($"teleporting: {other.name} ");
        }
        else
        {
            Debug.Log(temp.name + " : Has perished...");
            Destroy(temp);
        }
    }

}
