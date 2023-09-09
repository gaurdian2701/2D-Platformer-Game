using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject LevelList;
    private void Start()
    {
        HideLevelList();
    }
    public void ShowLevelList() => LevelList.SetActive(true);

    public void HideLevelList() => LevelList.SetActive(false);

    public void LoadLevel(string levelName) => SceneManager.LoadScene(levelName);

    public void QuitGame() => Application.Quit();
}
