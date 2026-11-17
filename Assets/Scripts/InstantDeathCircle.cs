using UnityEngine;

public class InstantDeathCircle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);

            GameStateManager.Instance.PlayerDied();
        }
    }
}