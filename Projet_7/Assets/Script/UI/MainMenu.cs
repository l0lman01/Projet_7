using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGameAdventure()
    {
        SceneManager.LoadScene("Lvl1");
    }
    public void PlayGamePuzzle()
    {
        SceneManager.LoadScene("Lvl2");
    }

    public void SceneMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
