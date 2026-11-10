using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    public static TestGameManager Instance { get; private set; }


    private void Awake()
    {
        // Check if a GameManager already exists in the scene
        if (Instance == null)
        {
            // If not, make this the official one and make it persist
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If one already exists, destroy this new duplicate instantly
            Destroy(gameObject);
        }
    }
    public void GlassGM()
    {
        Destroy(gameObject);
    }

}

