using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalHandler : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    [SerializeField] private SceneSelectorEnum targetScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            SceneManager.LoadScene((int)targetScene);
        }
    }
}
