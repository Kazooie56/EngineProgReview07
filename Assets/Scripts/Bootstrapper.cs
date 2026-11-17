using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    private void Start()
    {
        // Each manager already runs on Awake, so this runs on start, after all other managers are created.
        if (!ValidateManagers())
        {
            // if we're mising one, stop, don't load the MainMenu
            Debug.Log("The Managers are not valid, probably missing");
            return;
        }

        // Need to initialize the UIManager first since GameStateManager needs it to exist first.
        UIManager.Instance.Initialize();

        Debug.Log("All managers found, loading Main Menu and setting game state.");

        // Loads the MainMenu and sets the game state to mainmenu.
        LevelManager.Instance.LoadMainMenu();
        GameStateManager.Instance.SetState(GameState.MainMenu);
    }

    private bool ValidateManagers()
    {
        // validatedManager is true by default
        bool validatedManager = true;

        // If any of the managers are missing,
        // The method will say we didn't validate every manager and return it as false
        if (GameStateManager.Instance == null)
        {
            Debug.Log("Where's the GameStateManager bro?");
            validatedManager = false;
        }
        if (LevelManager.Instance == null)
        {
            Debug.Log("Where's the LevelManager bro?");
            validatedManager = false;
        }
        if (UIManager.Instance == null)
        {
            Debug.Log("Where's the UIManager bro?");
            validatedManager = false;
        }

        return validatedManager;
    }
}