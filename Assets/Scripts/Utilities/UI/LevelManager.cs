using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject GameOverScreen;
    private void Start()
    {
        KillZone.PlayerFallen += ShowGameOverScreen;
        PlayerHealth.PlayerDead += ShowGameOverScreen;
        GameOverScreen.SetActive(false);
    }

    private void OnDestroy()
    {
        KillZone.PlayerFallen -= ShowGameOverScreen;
        PlayerHealth.PlayerDead -= ShowGameOverScreen;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
            SceneManager.LoadScene("Level1");
    }

    private void ShowGameOverScreen() => GameOverScreen.SetActive(true);

    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void LoadLobbyScene() => SceneManager.LoadScene("Lobby");

}
