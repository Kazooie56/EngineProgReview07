using UnityEngine;

// this exists for every scene and contains each and every UI within it
public class UIPanelSender : MonoBehaviour
{
    // Only add whatever screens are relevant to each scene
    [SerializeField] private GameObject titleScreenUI;
    [SerializeField] private GameObject hudUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject resultsUI;

    // Sends information of each UI that's contained within a scene.
    private void Start()
    {
        UIManager.Instance.RegisterScreens(titleScreenUI, hudUI, pauseUI, gameOverUI, resultsUI);
    }
}