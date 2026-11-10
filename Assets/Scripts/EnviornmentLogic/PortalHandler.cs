using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalHandler : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    [SerializeField] private SceneSelectorEnum targetScene;
    [SerializeField] private Material easyMat;
    [SerializeField] private Material hardMat;
    [SerializeField] private bool isEasy = false;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private MeshRenderer meshRenderer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            SceneManager.LoadScene((int)targetScene);
        }
    }

    private void OnEnable()
    {
        GlobalEventManager.OnToggleLever += GlobalEventManager_OnToggleLever;
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider.enabled = false; //no dungeon selected
       
    }
    private void OnDisable()
    {
        GlobalEventManager.OnToggleLever -= GlobalEventManager_OnToggleLever;
    }

    private void GlobalEventManager_OnToggleLever(LeverSwitchSO obj)
    {
        boxCollider.enabled = true;
        isEasy = !isEasy;
        if (isEasy)
        {
            meshRenderer.material = easyMat;
            targetScene = SceneSelectorEnum.TestScene;
        }
        else
        {
            meshRenderer.material= hardMat;
            targetScene = SceneSelectorEnum.TestSceneApple;
        }
    }
}
