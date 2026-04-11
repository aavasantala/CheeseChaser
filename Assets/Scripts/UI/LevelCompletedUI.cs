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
    private VisualElement screen;

    public MouseController mouseController;

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
        screen = root.Q<VisualElement>("screen");

        // Safety checks and assign handlers
        if (btnRestart != null) btnRestart.clicked += RestartLevel;
        else Debug.LogError($"[LevelCompletedUI] Button '{restartName}' not found in UXML.");

        if (btnMainMenu != null) btnMainMenu.clicked += GoToMainMenu;
        else Debug.LogError($"[LevelCompletedUI] Button '{mainMenuName}' not found in UXML.");

        if (btnNextLevel != null) btnNextLevel.clicked += NextLevel;
        else Debug.LogError($"[LevelCompletedUI] Button '{nextLevelName}' not found in UXML.");

        // Hide by default
        root.style.display = DisplayStyle.None;
        //root.style.scale = new Scale(new Vector3(0.3f, 0.3f, 0.3f));

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
        
        StartCoroutine(showUI(0.1f, 0));

        

        
        
    }

    /// <summary>Hide the UI and resume the game.</summary>
    public void Hide()
    {
        if (root == null) return;

        StartCoroutine(showUI(0.1f, 1));
        
    }

    private void RestartLevel()
    {
        state = 1;
        Debug.Log("Restart level");
        float duration = 2f;
        mouseController.enabled = false;
        StartCoroutine(waiter(duration,state));
        
    }

    private void GoToMainMenu()
    {
        state = 2;
        float duration = 2f;
        mouseController.enabled = false;
        StartCoroutine(waiter(duration, state));
    }
    //this needs to refer to the next level
    private void NextLevel()
    {
        state = 3;
        float duration = 2f;
        mouseController.enabled = false;
        StartCoroutine(waiter(duration, state));
    }
    IEnumerator showUI(float duration, int states)
    {
        float elapsed0 = 0f;
        float interval = 0.016f;
        switch (states)
        {
            
            case 0:
                root.style.display = DisplayStyle.Flex;
                
                while (elapsed0 < duration)
                {
                    elapsed0 += interval;
                    float t0 = Mathf.Clamp01(elapsed0 / duration);
                    float s0 = Mathf.Lerp(0.3f, 1f, t0);
                    root.style.scale = new Scale(new Vector3(s0, s0, s0));
                    yield return new WaitForSecondsRealtime(interval);
                }
                root.style.scale = new Scale(new Vector3(1f, 1f, 1f));
                Time.timeScale = 0f;
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                UnityEngine.Cursor.visible = true;
                break;

            case 1:
                Time.timeScale = 1f;
                float elapsed1 = 0f;
                while (elapsed1 < duration)
                {
                    elapsed1 += interval;
                    float t1 = Mathf.Clamp01(elapsed1 / duration);
                    float s1 = Mathf.Lerp(1f, 0.3f, t1);
                    root.style.scale = new Scale(new Vector3(s1, s1, s1));
                    yield return new WaitForSecondsRealtime(interval);
                }
                root.style.scale = new Scale(new Vector3(0.3f, 0.3f, 0.3f));
                root.style.display = DisplayStyle.None;
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                UnityEngine.Cursor.visible = false;
                break;
        }
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
