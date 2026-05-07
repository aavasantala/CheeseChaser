using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class GameManager : MonoBehaviour
{
    public UIDocument Fader;
    public Image fade;
    public Tutorial tutorial;

    public MouseController mouseController;
    public static GameManager Instance { get; private set; }
    public enum GameState { Gameplay, GamePause, GameEnd }
    public GameState CurrentGameState
    {
        get => _currentGameState;
        set
        {
            _currentGameState = value;
            OnGameStateChange?.Invoke(_currentGameState);
        }
    }
    public UnityAction<GameState> OnGameStateChange;
    private GameState _currentGameState;


    private void Update()
    {
        if (CurrentGameState == GameState.Gameplay)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CurrentGameState = GameState.GamePause;
            }
        }
        else if(CurrentGameState == GameState.GamePause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CurrentGameState = GameState.Gameplay;
            }
            
        }

        
    }

    private void Start()
    {
        mouseController.enabled = true;
        Fader.enabled = true;
        var roost = Fader.rootVisualElement;
        fade = roost.Q<Image>("fade");
        roost.pickingMode=PickingMode.Ignore;
        fade.pickingMode=PickingMode.Ignore;
        
        float duration = 2f;
        StartCoroutine(waiter(duration));
        
        
        
    }
    private void OnEnable()
    {
        
    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        OnGameStateChange += OnGameStateChange_Callback;

        CurrentGameState = GameState.Gameplay;

        
    }

    private void OnGameStateChange_Callback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Gameplay:
                Time.timeScale = 1f;
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                UnityEngine.Cursor.visible = false;
                break;
            case GameState.GameEnd:
                Time.timeScale = 0f;
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                UnityEngine.Cursor.visible = true;
                break;
            case GameState.GamePause:
                Time.timeScale = 0f;
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                UnityEngine.Cursor.visible = true;
                break;
            default:
                Time.timeScale = 1f;
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                UnityEngine.Cursor.visible = true;
                Debug.LogWarning($"Game state not defined {gameState}");
                break;
        }
    }
    IEnumerator waiter(float duration)
    {
        fade.style.opacity = 1;
        Debug.Log($"Started at {Time.time}, waiting for {duration} seconds");
        yield return new WaitForSeconds(duration);
        Debug.Log($"Ended at {Time.time}");
        fade.style.opacity = 0;
        yield return new WaitForSeconds(duration);

        tutorial.StartTutorial();
    }
}

