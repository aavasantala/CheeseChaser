using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Objects")] 
    [SerializeField] private UIDocument inGameUI;
    [SerializeField] private UIDocument levelCompletedUI;
    [SerializeField] private UIDocument pauseUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChange += OnGameStateChange;
        ShowInGameUI(true);
    }

    private void HideAllUI()
    {
        inGameUI.rootVisualElement.style.display = DisplayStyle.None;
        levelCompletedUI.rootVisualElement.style.display = DisplayStyle.None;
        pauseUI.rootVisualElement.style.display = DisplayStyle.None;
    }


    public void ShowInGameUI(bool show)
    {
        HideAllUI();
        inGameUI.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    public void ShowEndGameUI(bool show)
    {
        HideAllUI();
        levelCompletedUI.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    public void ShowPauseUI(bool show)
    {
        HideAllUI();
        pauseUI.rootVisualElement.style.display= DisplayStyle.Flex;
    }
    
    private void OnGameStateChange(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.GameEnd:
                ShowEndGameUI(true);
                break;
            case GameManager.GameState.Gameplay:
                ShowInGameUI(true);
                break;
            case GameManager.GameState.GamePause:
                ShowPauseUI(true);
                break;
            default:
                Debug.LogWarning($"Current game state {state} is not implemented");
                break;
        }
    }

    public UIDocument GetInGameUI() => inGameUI;
    public UIDocument GetEndGameUI() => levelCompletedUI;

    public UIDocument GetPauseUI() => pauseUI;
}
