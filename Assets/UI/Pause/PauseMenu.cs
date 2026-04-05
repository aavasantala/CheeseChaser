using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static GameManager;

public class PauseMenu : MonoBehaviour
{
    public GameManager Manager;

    private Button resumeBtn;
    private Button mainmenuBtn;
    private Button quitBtn;
    
    void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument component missing!");
            return;
        }
        var root = uiDocument.rootVisualElement;

        resumeBtn = root.Q<Button>("resumeBtn");
        mainmenuBtn = root.Q<Button>("mainmenuBtn");
        quitBtn = root.Q<Button>("quitBtn");

        if (resumeBtn != null)
            resumeBtn.clicked += BackToGame;
        else
            Debug.LogError("resumeBtn not found!");

        if (mainmenuBtn != null)
            mainmenuBtn.clicked += BackToMain;
        else
            Debug.LogError("mainmenuBtn not found!");

        if (quitBtn != null)
            quitBtn.clicked += QuitGame;
        else
            Debug.LogError("quitBtn not found!");
    }

    void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void BackToGame()
    {
        Manager.CurrentGameState = GameState.Gameplay;
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
