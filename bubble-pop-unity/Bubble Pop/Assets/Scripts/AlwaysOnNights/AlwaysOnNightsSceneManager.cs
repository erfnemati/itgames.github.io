using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AlwaysOnNightsSceneManager : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        AlwaysOnNigthsGameManager._instance.PauseGame();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        AlwaysOnNigthsGameManager._instance.ResumeGame();
    }

    public void RestartLevel()
    {
        int currentLevelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentLevelIndex);
    }

    public void LoadNextLevel()
    {
        int currentLevelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = (currentLevelIndex + 1) % UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelIndex);
    }

    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ShowWinningScreen()
    {
        Debug.Log("You have won");
        Time.timeScale = 0.0f;
    }

    public void ShowLosingScreen()
    {
        Debug.Log("Losing");
        Time.timeScale = 1.0f;
    }
}
