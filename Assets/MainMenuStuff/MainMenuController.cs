using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private Button btnStartGame;
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
        btnQuit = root.Q<Button>("btnQuit");

        if (btnStartGame != null)
            btnStartGame.clicked += StartGame;
        else
            Debug.LogError("btnStartGame not found!");

        if (btnQuit != null)
            btnQuit.clicked += QuitGame;
        else
            Debug.LogError("btnQuit not found!");
    }

    void StartGame()
    {
        Debug.Log("Start Game clicked");
        SceneManager.LoadScene("Tutorial");
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
}
