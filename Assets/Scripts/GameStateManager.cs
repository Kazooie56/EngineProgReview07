using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState
{
    MainMenu,  // separated Menu and Results into different gamestates
    Playing,
    Paused,
    GameOver,
    Results
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    // The UIManager subscribes to this so it can react to every state change.
    public event Action<GameState> OnStateChanged;

    public GameState CurrentState = GameState.MainMenu;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
        {
            return;
        }

        if (keyboard.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;

        if (newState == GameState.Paused || newState == GameState.GameOver)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        // Sends an "OnStateChanged" message to UIManager,
        // which is the only thing that listens to this invoke
        OnStateChanged.Invoke(newState);
    }

    // since it only checks if we're paused or playing, It only applies to during gameplay and when it's paused.
    public void TogglePause()
    {
        if (CurrentState == GameState.Playing)
        {
            SetState(GameState.Paused);
        }
        else if (CurrentState == GameState.Paused)
        {
            SetState(GameState.Playing);
        }
    }

    // StartGame also doubles as a "restart gameplay" button/feature too
    public void StartGame()
    {
        LevelManager.Instance.LoadGameplay();
        SetState(GameState.Playing);
    }

    // called when the player touched the goal
    // move to the results screen
    public void GoalReached()
    {
        LevelManager.Instance.LoadResults();
        SetState(GameState.Results);
    }

    // called only on the results screen
    // move to the main menu
    public void GoToMenu()
    {
        LevelManager.Instance.LoadMainMenu();
        SetState(GameState.MainMenu);
    }

    // sets the state as GameOver, which causes the GameOver gamestate to trigger
    public void PlayerDied()
    {
        SetState(GameState.GameOver);
    }
}