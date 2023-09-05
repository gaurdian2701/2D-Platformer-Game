using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private void Start()
    {
        KillZone.PlayerFallen += ReloadScene;
    }

    private void OnDestroy()
    {
        KillZone.PlayerFallen -= ReloadScene;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            SceneManager.LoadScene("Level1");
    }

    void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
