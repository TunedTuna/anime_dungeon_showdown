using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuBtn;
    private void Start()
    {
        GlobalEventManager.OnGameOver += GlobalEventManager_OnGameOver;
        HideUI();
        mainMenuBtn.onClick.AddListener(() => { ReturnToMainMenu(); });
        
    }
  

    public void HideUI()//now part of contract
    {
        gameObject.SetActive(false);
    }
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

    private void GlobalEventManager_OnGameOver()
    {
        ShowUI();
    }
    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene((int)SceneSelectorEnum.MainMenuVer2);
    }

    private void OnDestroy()
    {
        mainMenuBtn.onClick.RemoveAllListeners();
        GlobalEventManager.OnGameOver -= GlobalEventManager_OnGameOver;
    }
}
