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
    public void ShowLevelList()
    {
        LevelList.SetActive(true);
        SoundManager.Instance.PlaySound(Sounds.ButtonClick);
    }

    public void HideLevelList()
    {
        LevelList.SetActive(false);
        SoundManager.Instance.PlaySound(Sounds.ButtonClick);
    }

    public void QuitGame()
    {
        SoundManager.Instance.PlaySound(Sounds.ButtonClick);
        Application.Quit();
    }
}
