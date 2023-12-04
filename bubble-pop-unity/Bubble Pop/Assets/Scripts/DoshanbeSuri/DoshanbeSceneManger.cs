using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class DoshanbeSceneManger : MonoBehaviour,TheSceneManager
{
    [SerializeField] WheelOfLuckController m_wheelOfLuck;
    public void PauseGame()
    {
        Time.timeScale = 0f;
        m_wheelOfLuck.SetIsGamePaused(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        m_wheelOfLuck.SetIsGamePaused(false);
    }

    public void LoadNextLevel()
    {
        int nextLevel = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevel);
    }
}
