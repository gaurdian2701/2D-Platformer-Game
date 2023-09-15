using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject LevelCompleteScreen;

    private int maxScenes = 5;

    private void Start()
    {
        KillZone.PlayerFallen += ShowGameOverScreen;
        PlayerHealth.PlayerDead += ShowGameOverScreen;
        GameOverScreen.SetActive(false);
        LevelCompleteScreen.SetActive(false);
    }

    private void OnDestroy()
    {
        KillZone.PlayerFallen -= ShowGameOverScreen;
        PlayerHealth.PlayerDead -= ShowGameOverScreen;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            LevelManager.Instance.MarkLevelComplete(SceneManager.GetActiveScene().name);
            LevelCompleteScreen.SetActive(true);
            other.gameObject.GetComponent<PlayerController>().LevelComplete();
        }
    }

    private void ShowGameOverScreen()
    {
        GameOverScreen.SetActive(true);
        SoundManager.Instance.PlaySound(Sounds.DeathMusic);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SoundManager.Instance.PlaySound(Sounds.ButtonClick);
    }

    public void LoadLobbyScene() => SceneManager.LoadScene("Lobby");

    public void LoadLevel(string levelName)
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(levelName);

        switch (levelStatus)
        {
            case LevelStatus.Locked:
                Debug.Log("LEVEL LOCKED\n");
                break;

            case LevelStatus.Unlocked:
                SoundManager.Instance.PlaySound(Sounds.NextLevel);
                SceneManager.LoadScene(levelName);
                break;

            case LevelStatus.Completed:
                SoundManager.Instance.PlaySound(Sounds.NextLevel);
                SceneManager.LoadScene(levelName);
                break;

            default:
                break;
        }
    }

    public void LoadNextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        string nextLevel = "Level" + index;

        if (index > maxScenes)
            LoadLobbyScene();

        else
            LoadLevel(nextLevel);

    }

}
