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
    public static LevelManager m_instance;
    private int m_numOfAchievedAnars;

    public void Start()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else if (m_instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void UpdateGoalUi()
    {
        goalText.text = m_numOfAchievedAnars + "/" + m_numOfGoalAnars;
    }

    public void IncreaseAchievedAnar()
    {
        m_numOfAchievedAnars++;
        UpdateGoalUi();
        if (isLevelFinished())
        {
            m_player.GoToSpace();
        }
        
    }

    public bool isLevelFinished()
    {
        if (m_numOfAchievedAnars >= m_numOfGoalAnars)
        {
            return true;
        }
        return false;
    }

    

    public void RestartLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }
}
