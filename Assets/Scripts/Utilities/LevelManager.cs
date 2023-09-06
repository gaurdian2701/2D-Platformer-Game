using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private void Start()
    {
        KillZone.PlayerFallen += InitiateSceneReload;
        PlayerHealth.PlayerDead += InitiateSceneReload;
    }

    private void OnDestroy()
    {
        KillZone.PlayerFallen -= InitiateSceneReload;
        PlayerHealth.PlayerDead -= InitiateSceneReload;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            SceneManager.LoadScene("Level1");
    }

    private void InitiateSceneReload() => Invoke("ReloadScene", 3f);

    private void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

}
