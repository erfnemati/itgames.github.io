using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] TMP_Text goalText;
    [SerializeField] int m_numOfGoalAnars;
    [SerializeField] PlayerController m_player;
    [SerializeField] GameObject m_grayScreen;
    [SerializeField] GameObject m_resultMenu;
    [SerializeField] GameObject m_continueButton;
    [SerializeField] GameObject m_restartPanel;
    [SerializeField] GameObject m_middleStar;
    [SerializeField] GameObject RewardPanel;
    [SerializeField] GameObject anarwinvfx;
    public static LevelManager m_instance;
    public AudioSource catchscore, win;

    private int m_numOfAchievedAnars;
    public int m_obstgen;

    [SerializeField] GameObject m_leftPanel;
    [SerializeField] GameObject m_rightPanel;
    public void Start()
    {
        Time.timeScale = 1.0f;

        if (m_instance == null)
        {
            m_instance = this;
        }
        else if (m_instance != this)
        {
            Destroy(this.gameObject);
        }

        UpdateGoalUi();
    }

    private void Update()
    {
        
    }

    private void UpdateGoalUi()
    {
        goalText.text = m_numOfGoalAnars + "/" + m_numOfAchievedAnars;
    }

    public void IncreaseAchievedAnar()
    {
        catchscore.Play();
        m_numOfAchievedAnars++;
        UpdateGoalUi();
        if (isLevelFinished())
        {
            PassLevel();
        }
    }
    //------------------------------------------------------------
    public bool isLevelFinished()
    {
        if (m_numOfAchievedAnars >= m_numOfGoalAnars)
        {
            FinishLevel();
            return true;
        }
        return false;
    }
    public void EndAnarGeneration()
    {
        Time.timeScale = 0.0f;
        m_grayScreen.SetActive(true);
        m_resultMenu.SetActive(true);
        RewardPanel.SetActive(false);
        m_continueButton.SetActive(false);
        m_restartPanel.SetActive(true);
    }

    public void PassLevel()
    {
        
        EndLevel();
        anarwinvfx.SetActive(true);
        win.Play();
        Invoke(nameof(ShowWinningScreen), 2f);
        
    }

    private void ShowWinningScreen()
    {
        m_grayScreen.SetActive(true);
        m_resultMenu.SetActive(true);
        m_continueButton.SetActive(true);
        m_restartPanel.SetActive(false);
        m_middleStar.SetActive(true);
        RewardPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void EndLevel()
    {
        FindObjectOfType<ObstacleGenerator>().gameObject.SetActive(false);
        Anar[] anarList = FindObjectsOfType<Anar>();
        Obstacle[] obstacleList = FindObjectsOfType<Obstacle>();
        foreach(Anar temp in anarList)
        {
            temp.gameObject.SetActive(false);
        }

        foreach(Obstacle tempObstacle in obstacleList)
        {
            tempObstacle.gameObject.SetActive(false);
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        int currentLevelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentLevelIndex);
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void FailLevel()
    {
        Time.timeScale = 0.0f;
        m_grayScreen.SetActive(true);
        m_resultMenu.SetActive(true);
        m_continueButton.SetActive(false);
        RewardPanel.SetActive(false);
        //---
        m_restartPanel.SetActive(true);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = (currentLevelIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevelIndex);
    }

    private void FinishLevel()
    {
        m_leftPanel.gameObject.SetActive(false);
        m_rightPanel.gameObject.SetActive(false);
        m_player.SetIsGameOver(true);
    }
}
