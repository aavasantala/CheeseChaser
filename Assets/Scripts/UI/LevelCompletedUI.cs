using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelCompletedUI : MonoBehaviour
{
    [Header("Assign the UIDocument (the component that uses your UXML)")]
    [SerializeField] private UIDocument uiDocument;

    public UIDocument Fader;
    public Image fade;
    public int state;

    private VisualElement root;
    private Button btnRestart;
    private Button btnMainMenu;
    private Button btnNextLevel;

    private const string restartName = "btnRestart";
    private const string mainMenuName = "btnMainMenu";
    private const string nextLevelName = "btnNextLevel";

    private void Awake()
    {
        if (uiDocument == null)
        {
            Debug.LogError("[LevelCompletedUI] UIDocument reference is not assigned in the inspector. Please assign it.");
            enabled = false;
            return;
        }

        // Get root
        root = uiDocument.rootVisualElement;
        if (root == null)
        {
            Debug.LogError("[LevelCompletedUI] rootVisualElement is null. Make sure the UIDocument has a valid Source Asset (UXML).");
            enabled = false;
            return;
        }

        // Query buttons (UI Toolkit queries by name)
        btnRestart = root.Q<Button>(restartName);
        btnMainMenu = root.Q<Button>(mainMenuName);
        btnNextLevel = root.Q<Button>(nextLevelName);

        // Safety checks and assign handlers
        if (btnRestart != null) btnRestart.clicked += RestartLevel;
        else Debug.LogError($"[LevelCompletedUI] Button '{restartName}' not found in UXML.");

        if (btnMainMenu != null) btnMainMenu.clicked += GoToMainMenu;
        else Debug.LogError($"[LevelCompletedUI] Button '{mainMenuName}' not found in UXML.");

        if (btnNextLevel != null) btnNextLevel.clicked += NextLevel;
        else Debug.LogError($"[LevelCompletedUI] Button '{nextLevelName}' not found in UXML.");

        // Hide by default
        root.style.display = DisplayStyle.None;

        var roost = Fader.rootVisualElement;
        fade = roost.Q<Image>("fade");
    }

    private void OnDisable()
    {
        // unsubscribe safely
        if (btnRestart != null) btnRestart.clicked -= RestartLevel;
        if (btnMainMenu != null) btnMainMenu.clicked -= GoToMainMenu;
        if (btnNextLevel != null) btnNextLevel.clicked -= NextLevel;
    }

    /// <summary>Show the Level Completed UI and pause the game.</summary>
    public void Show()
    {
        if (root == null) return;

        root.style.display = DisplayStyle.Flex;

        Time.timeScale = 0f;

        // unlock cursor for clicking UI
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    /// <summary>Hide the UI and resume the game.</summary>
    public void Hide()
    {
        if (root == null) return;

        root.style.display = DisplayStyle.None;

        Time.timeScale = 1f;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    private void RestartLevel()
    {
        state = 1;
        Debug.Log("Restart level");
        float duration = 2f;
        StartCoroutine(waiter(duration,state));
        
    }

    private void GoToMainMenu()
    {
        state = 2;
        float duration = 2f;
        StartCoroutine(waiter(duration, state));
    }
    //this needs to refer to the next level
    private void NextLevel()
    {
        state = 3;
        float duration = 2f;
        StartCoroutine(waiter(duration, state));
    }

    IEnumerator waiter(float duration, int state)
    {
        Time.timeScale = 1f;
        fade.style.opacity = 1;
        Debug.Log($"Started at {Time.time}, waiting for {duration} seconds");
        yield return new WaitForSeconds(duration);
        Debug.Log($"Ended at {Time.time}");
        
        

        switch (state) 
        {
            case 1:
                Debug.Log("Reeeestart");
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;

            case 2:
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
                break;

            case 3:
                Debug.Log("[LevelCompletedUI] Next Level button pressed. Not implemented.");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1f;
                break;

            default:
                Debug.Log("Error, state invalid -> " + state);
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
                break;

        }

    }
}
