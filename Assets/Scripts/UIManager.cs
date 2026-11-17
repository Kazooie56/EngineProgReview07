using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    // Holds information for each UI in the game
    private GameObject mainMenuUI;
    private GameObject hudUI;
    private GameObject pauseScreenUI;
    private GameObject gameOverUI;
    private GameObject resultsUI;

    // singleton
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

    public void Initialize()
    {
        GameStateManager.Instance.OnStateChanged += HandleStateChanged;
    }

    // unsubscribe
    private void OnDestroy()
    {
        if (Instance == this && GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnStateChanged -= HandleStateChanged;
        }
    }



    // every time a scene is loaded, it passes each UI it has thanks to the UIPanelSender
    // UIManager only updates the UI for UI within a scene, everything else is returned as null.
    // The older UI becomens a destroyed object because the scene containing it was unloaded, treating them as null, which skips those
    public void RegisterScreens(GameObject mainMenu, GameObject hud, GameObject pause, GameObject gameOver, GameObject results)
    {
        if (mainMenu != null)
        {
            mainMenuUI = mainMenu;
        }
        if (hud != null)
        {
            hudUI = hud;
        }
        if (pause != null)
        {
            pauseScreenUI = pause;
        }
        if (gameOver != null)
        {
            gameOverUI = gameOver;
        }
        if (results != null)
        {
            resultsUI = results;
        }

        // The scene loads AFTER the state changed, so sync right away.
        RefreshUI();
    }

    // Whenever GameStateManager sends out an event,
    // which only happens when it changes state, call RefreshUI()
    private void HandleStateChanged(GameState newState)
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        GameState state = GameStateManager.Instance.CurrentState;


        // For each panel that exists in the scene, disable it if it isn't GameState state, enable the one that matches the state
        if (mainMenuUI != null)
        {
            mainMenuUI.SetActive(state == GameState.MainMenu);  // main menu
        }

        if (hudUI != null)
        {
            hudUI.SetActive(state == GameState.Playing);        // HUD for coin counter, rules, and controls
        }

        if (pauseScreenUI != null)
        {
            pauseScreenUI.SetActive(state == GameState.Paused); // HUD for pause menu
        }

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(state == GameState.GameOver);  // HUD for game over menu
        }

        if (resultsUI != null)
        {
            resultsUI.SetActive(state == GameState.Results);    // HUD for results
        }
    }
}