using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseUI : MonoBehaviour, IUserInterface
{
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button mainMenuBtn;

    [Header("Screens?")]
    [SerializeField] private GameObject settingsScreen;

    private void OnEnable()
    {
        resumeBtn.onClick.AddListener(() => { ResumeGame(); });
        settingsBtn.onClick.AddListener(() => {OpenSettings(); });
        mainMenuBtn.onClick.AddListener(() => {ReturnToMainMenu(); });
    }
    private void ResumeGame()
    {
        //mess with the game time thing
        HideUI();
    }
    private void OpenSettings()
    {
        settingsScreen.SetActive(true);
    }
    private void ReturnToMainMenu()
    {
        TestGameManager.Instance.GlassGM();
        SceneManager.LoadScene((int)SceneSelectorEnum.MainMenuVer2);
    }
    public void HideUI()//now part of contract
    {
        gameObject.SetActive(false);
    }
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }
}
