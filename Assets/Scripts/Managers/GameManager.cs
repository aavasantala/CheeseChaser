using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
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
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case GameState.GameEnd:
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.GamePause:
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            default:
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Debug.LogWarning($"Game state not defined {gameState}");
                break;
        }
    }
}
