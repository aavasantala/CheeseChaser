using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public LevelCompletedUI levlcom;

    [Header("UI Objects")]
    [SerializeField] private UIDocument inGameUI;
    [SerializeField] private UIDocument levelCompletedUI;
    [SerializeField] private UIDocument pauseUI;

    private float animDuration = 0.2f;
    private float interval = 0.016f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChange += OnGameStateChange;
        HideAllUI();
        inGameUI.rootVisualElement.pickingMode = PickingMode.Ignore;
    }

    public void HideAllUI()
    {
        StartCoroutine(AnimateScaleOut(levelCompletedUI.rootVisualElement));
        StartCoroutine(AnimateScaleOut(pauseUI.rootVisualElement));
        
    }

    public void ShowInGameUI(bool show)
    {
        HideAllUI();
        inGameUI.rootVisualElement.style.display = DisplayStyle.Flex;
        StartCoroutine(AnimateOpacity(inGameUI.rootVisualElement, 0f, 1f));
    }

    public void ShowEndGameUI(bool show)
    {
        HideAllUI();
        StartCoroutine(AnimateOpacity(inGameUI.rootVisualElement, 1f, 0f));
        levlcom.Show();
    }

    public void ShowPauseUI(bool show)
    {
        HideAllUI();
        StartCoroutine(AnimateOpacity(inGameUI.rootVisualElement, 1f, 0f));
        StartCoroutine(AnimateScaleIn(pauseUI.rootVisualElement));
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

    IEnumerator AnimateScaleIn(VisualElement target)
    {
        target.style.scale = new Scale(new Vector3(0.3f, 0.3f, 0.3f));
        target.style.display = DisplayStyle.Flex;
        float elapsed = 0f;
        while (elapsed < animDuration)
        {
            elapsed += interval;
            float t = Mathf.Clamp01(elapsed / animDuration);
            float s = Mathf.Lerp(0.3f, 1f, t);
            target.style.scale = new Scale(new Vector3(s, s, s));
            yield return new WaitForSecondsRealtime(interval);
        }
        target.style.scale = new Scale(new Vector3(1f, 1f, 1f));
    }

    IEnumerator AnimateScaleOut(VisualElement target)
    {
        if (target.style.display == DisplayStyle.None) yield break;
        float elapsed = 0f;
        while (elapsed < animDuration)
        {
            elapsed += interval;
            float t = Mathf.Clamp01(elapsed / animDuration);
            float s = Mathf.Lerp(1f, 0.3f, t);
            target.style.scale = new Scale(new Vector3(s, s, s));
            yield return new WaitForSecondsRealtime(interval);
        }
        target.style.scale = new Scale(new Vector3(0.3f, 0.3f, 0.3f));
        target.style.display = DisplayStyle.None;
    }

    IEnumerator AnimateOpacity(VisualElement target, float from, float to, System.Action onComplete = null)
    {
        float elapsed = 0f;
        target.style.opacity = from;
        while (elapsed < animDuration)
        {
            elapsed += interval;
            float t = Mathf.Clamp01(elapsed / animDuration);
            target.style.opacity = Mathf.Lerp(from, to, t);
            yield return new WaitForSecondsRealtime(interval);
        }
        target.style.opacity = to;
        onComplete?.Invoke();
    }

    public UIDocument GetInGameUI() => inGameUI;
    public UIDocument GetEndGameUI() => levelCompletedUI;
    public UIDocument GetPauseUI() => pauseUI;
}