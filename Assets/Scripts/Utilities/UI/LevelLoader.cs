using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject LevelCompleteScreen;

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

    private void ShowGameOverScreen() => GameOverScreen.SetActive(true);

    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void LoadLobbyScene() => SceneManager.LoadScene("Lobby");

    public void LoadLevel(string  levelName)
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(levelName);

        switch(levelStatus)
        {
            case LevelStatus.Locked:
                Debug.Log("LEVEL LOCKED\n");
                break;

            case LevelStatus.Unlocked:
                SceneManager.LoadScene(levelName);
                break;

            case LevelStatus.Completed:
                SceneManager.LoadScene(levelName);
                break;

            default:
                break;
        }
    }

    public void LoadNextLevel()
    {
        string nextLevel = "Level" + (SceneManager.GetActiveScene().buildIndex + 1);

        if (!SceneManager.GetSceneByName(nextLevel).IsValid())
            LoadLobbyScene();
        else
            LoadLevel(nextLevel);
    }

}
