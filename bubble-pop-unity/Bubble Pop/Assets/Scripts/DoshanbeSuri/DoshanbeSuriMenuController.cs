using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class DoshanbeSuriMenuController : MonoBehaviour
{
    private TheSceneManager m_currentSceneManager;
    [SerializeField] MenuUiController m_menuUiController;

    private void Start()
    {
        //This line should change every level
        m_currentSceneManager = FindObjectOfType<DoshanbeSceneManger>().GetComponent<DoshanbeSceneManger>();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        m_currentSceneManager.ResumeGame();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        m_currentSceneManager.RestartGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        m_currentSceneManager.PauseGame();
    }

    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1.0f;
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = (currentLevelIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevelIndex);
    }
}
