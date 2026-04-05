using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public UIDocument settingsUI;
    
    private Button btnStartGame;
    private Button btnSettings;
    private Button btnQuit;
    
    void OnEnable()
    {
        
        var uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument component missing!");
            return;
        }

        var root = uiDocument.rootVisualElement;
        

        btnStartGame = root.Q<Button>("btnStartGame");
        btnSettings = root.Q<Button>("btnSettings");
        btnQuit = root.Q<Button>("btnQuit");

        if (btnStartGame != null)
            btnStartGame.clicked += StartGame;
        else
            Debug.LogError("btnStartGame not found!");

        if (btnQuit != null)
            btnQuit.clicked += QuitGame;
        else

            Debug.LogError("btnSettings not found!");
        if (btnSettings != null)
            btnSettings.clicked += Settings;
        else
            Debug.LogError("btnSettings not found!");
    }

    void StartGame()
    {
        Debug.Log("Start Game clicked");
        SceneManager.LoadScene("Level1");
    }

    void QuitGame()
    {
        Debug.Log("Quit clicked");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void Settings()
    {
        Debug.Log("Settings clicked");
        
        settingsUI.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
