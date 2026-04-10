using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public UIDocument settingsUI;
    
    private Button btnStartGame;
    private Button btnSettings;
    private Button btnQuit;

    public UIDocument Fader;
    public Image fade;

    void OnEnable()
    {
        Fader.enabled = true;
        var uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument component missing!");
            return;
        }

        var root = uiDocument.rootVisualElement;
        var roost = Fader.rootVisualElement;
        fade = roost.Q<Image>("fade");
        



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

    private void Start()
    {
        StartCoroutine(TimeWaster(1f));
    }

    void StartGame()
    {
        Debug.Log("Start Game clicked");
        fade.style.opacity = 1;
        StartCoroutine(waiter(2, 1));
        
    }

    void QuitGame()
    {
        Debug.Log("Quit clicked");
        fade.style.opacity = 1;
        StartCoroutine(waiter(2, 2));

    }

    void Settings()
    {
        Debug.Log("Settings clicked");
        
        settingsUI.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    IEnumerator waiter(float duration, int state)
    {
        yield return new WaitForSeconds(duration);
        switch (state)
        {
            case 1:
                SceneManager.LoadScene("Level1");
                break;

            case 2:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();

#endif
                break;

        }
    }
    IEnumerator TimeWaster(float suration)
    {
        yield return null;
        fade.style.opacity = 1;

        yield return new WaitForSeconds(suration);
        fade.style.opacity = 0;
        yield return new WaitForSeconds(suration);
    }
}
