using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RTLTMPro;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] int m_thisLevelIndex;

    [SerializeField] SpriteRenderer m_firstStar;
    [SerializeField] SpriteRenderer m_secStar;
    [SerializeField] SpriteRenderer m_thirdStar;

    [SerializeField] Sprite m_turnedOnStar;
    [SerializeField] Button m_lockedLevelState;
    [SerializeField] Sprite m_turnedOnLevel;
    [SerializeField] Sprite m_turnedOffLevel;
    [SerializeField] Image m_thisLevelSpriteRenderer;
    [SerializeField] int m_indexOffset;
    [SerializeField] RTLTextMeshPro m_levelText;
    
    private void GetLevelInfo()
    {
        Debug.Log("index is : " + m_thisLevelIndex);
        int numOfStars = LevelSelectManager._levelSelectManagerInstance.
            GetLevelInfo(m_thisLevelIndex).GetNumOfStars();
        Debug.Log("Number of stars : " + numOfStars);

        bool isLevelSolved = LevelSelectManager._levelSelectManagerInstance.
            GetLevelInfo(m_thisLevelIndex).GetLockedState();
        Debug.Log("isLevelSolved : " + isLevelSolved);

        SetIsLevelLocked(isLevelSolved);
        SetLevelStarsUi(numOfStars);
    }

    private void Start()
    {
        SetLevelText();
        GetLevelInfo();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(m_thisLevelIndex);
    }

    private void SetLevelText()
    {
        m_levelText.text = (m_thisLevelIndex - m_indexOffset) + "";
    }

    private void SetLevelStarsUi(int numOfAchievedStars)
    {
        Debug.Log("Hello i am here with : " + numOfAchievedStars);
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
        int currentLevel = LevelSelectManager._levelSelectManagerInstance.GetCurrentLevel();
        if(m_thisLevelIndex == currentLevel)
        {
            m_lockedLevelState.interactable = true;
        }
        else if (solveState == true )
        {
            m_lockedLevelState.interactable = true;
            m_thisLevelSpriteRenderer.sprite = m_turnedOffLevel;
        }
        else
        {
            m_lockedLevelState.interactable = false;
        }
    }

    
}
