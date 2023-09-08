using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public void StartGame() => SceneManager.LoadScene("Level1");

    public void QuitGame() => Application.Quit();
}
