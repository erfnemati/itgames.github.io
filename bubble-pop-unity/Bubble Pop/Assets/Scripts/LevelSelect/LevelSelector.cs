using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] int m_thisLevelIndex;

    [SerializeField] SpriteRenderer m_firstStar;
    [SerializeField] SpriteRenderer m_secStar;
    [SerializeField] SpriteRenderer m_thirdStar;

    [SerializeField] Sprite m_turnedOnStar;
    [SerializeField] SpriteRenderer m_lockedLevelState;
    [SerializeField] Sprite m_turnedOnLevel;
    [SerializeField] Sprite m_turnedOffLevel;
    
    private void GetLevelInfo()
    {
        int numOfStars = LevelSelectManager._levelSelectManagerInstance.
            GetLevelInfo(m_thisLevelIndex).GetNumOfStars();

        bool isLevelSolved = LevelSelectManager._levelSelectManagerInstance.
            GetLevelInfo(m_thisLevelIndex).GetLockedState();

        SetIsLevelLocked(isLevelSolved);
        SetLevelStarsUi(numOfStars);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(m_thisLevelIndex);
    }

    private void SetLevelStarsUi(int numOfAchievedStars)
    {
        if (numOfAchievedStars == 1)
        {
            m_firstStar.sprite = m_turnedOnStar;
        }
        else if (numOfAchievedStars == 2)
        {
            m_firstStar.sprite = m_turnedOnStar;
            m_secStar.sprite = m_turnedOnStar;
        }
        else if (numOfAchievedStars == 3)
        {
            m_firstStar.sprite = m_turnedOnStar;
            m_secStar.sprite = m_turnedOnStar;
            m_thirdStar.sprite = m_turnedOnStar;
        }
    }

    private void SetIsLevelLocked(bool solveState)
    {
        if (solveState == true)
        {
            m_lockedLevelState.sprite = m_turnedOnLevel;
        }
        else
        {
            m_lockedLevelState.sprite = m_turnedOffLevel;
        }
    }

    
}
