using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button newGameButton_Tuna;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button CreditsButton;
    [SerializeField] private Button CreditsButton_Tuna;
    [SerializeField] private Button QuitButton;

    [Header("Screens")]
    [SerializeField] private GameObject SettingsScreen;
    [SerializeField] private GameObject CreditsScreen;
    [SerializeField] private GameObject CreditsTuna;

    [Header("ScreensBool")]
    [SerializeField] private bool isCreditsOpen;

    [Header("Misc")]
    [SerializeField] private SceneSelectorEnum targetScene;

    private void OnEnable()
    {
        newGameButton_Tuna.onClick.AddListener(() => { NewGameBtn_Clicked(); });
        settingsButton.onClick.AddListener(() => { OpenSettingsScreen(); });
        CreditsButton.onClick.AddListener(() => {ToggleCreditsScreen(); });
        CreditsButton_Tuna.onClick.AddListener(()=> { OpenCredits_Tuna(); });
        QuitButton.onClick.AddListener(() => { QuitGame(); });


    }
    private void OnDisable()
    {
        newGameButton_Tuna.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        CreditsButton.onClick.RemoveAllListeners();
        CreditsButton_Tuna.onClick.RemoveAllListeners();
        QuitButton.onClick.RemoveAllListeners();
    }



    private void NewGameBtn_Clicked()
    {
        SceneManager.LoadScene((int)targetScene);
    }
    private void OpenSettingsScreen()
    {
        SettingsScreen.SetActive(true);
    }
    private void ToggleCreditsScreen()
    {
        isCreditsOpen = !isCreditsOpen;
        CreditsScreen.SetActive(isCreditsOpen);
    }
    private void OpenCredits_Tuna()
    {
        CreditsTuna.SetActive(true);
    }
    private void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
    }





}
