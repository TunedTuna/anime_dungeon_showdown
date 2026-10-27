using UnityEngine;

public class CharacterSelectUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    // Update is called once per frame
 

    public void HideUI()
    {
        gameObject.SetActive(false);
    }
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        HideUI();
    }
    private void OnEnable()
    {
        ShowUI();
    }
}
