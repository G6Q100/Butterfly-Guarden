using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] string sceneName;

    public GameObject optionSettings;

    public void PauseGame()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            optionSettings.SetActive(true);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        optionSettings.SetActive(false);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
