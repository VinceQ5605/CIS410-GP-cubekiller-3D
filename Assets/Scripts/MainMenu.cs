using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayMountainW()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (PauseMenu.GameIsPaused)
        {
            Time.timeScale = 1f;
            PauseMenu.GameIsPaused = false;
        }
    }

    public void PlayDesertW()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        if (PauseMenu.GameIsPaused)
        {
            Time.timeScale = 1f;
            PauseMenu.GameIsPaused = false;
        }
    }

    public void PlaySnowW()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
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
