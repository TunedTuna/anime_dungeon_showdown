using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSwitch : MonoBehaviour {
    
    public GameObject OptionsHolder;
    [SerializeField] private SceneSelectorEnum targetScene;

    private void Awake()
    {
        Debug.Log(targetScene.ToString());
    }
    public void OnButtonClick()
    {
        
        SceneManager.LoadScene((int)targetScene);
        //This will be different in the final game
    }

    public void SettingsMenuOn() {
        OptionsHolder.SetActive (true);
    }
    
    public void SettingsMenuOff() {
        OptionsHolder.SetActive (false);
    }
}
