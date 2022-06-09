using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayMountainW()
    {
        SceneManager.LoadScene("MainScene");
        if (PauseMenu.GameIsPaused)
        {
            Time.timeScale = 1f;
            PauseMenu.GameIsPaused = false;
        }
    }

    public void PlayDesertW()
    {
        SceneManager.LoadScene("DesertWorld");
        if (PauseMenu.GameIsPaused)
        {
            Time.timeScale = 1f;
            PauseMenu.GameIsPaused = false;
        }
    }

    public void PlaySnowW()
    {
        SceneManager.LoadScene("SnowWorld");
        if (PauseMenu.GameIsPaused)
        {
            Time.timeScale = 1f;
            PauseMenu.GameIsPaused = false;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
