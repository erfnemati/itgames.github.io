using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager _levelSelectManagerInstance;
    private Dictionary<int, LevelInfo> m_levelList = new Dictionary<int, LevelInfo>();
    [SerializeField] int m_maxLevelIndex;
    [SerializeField] int m_minLevelIndex;
    [SerializeField] int m_currentLevel;

    private void Awake()
    {
        if (_levelSelectManagerInstance != null && _levelSelectManagerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _levelSelectManagerInstance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        ResetLevelList();
    }
   

    public void UpdateLevelInfo(int levelIndex,LevelInfo lvlInfo)
    {
        if (m_levelList.ContainsKey(levelIndex))
        {
            m_levelList[levelIndex] = lvlInfo;
        }
        else
        {
            m_levelList.Add(levelIndex, lvlInfo);
        }
    }

    public LevelInfo GetLevelInfo(int levelIndex)
    {
        if (levelIndex < m_minLevelIndex || levelIndex > m_maxLevelIndex)
        {
            Debug.Log("Levelindex is wrong");
            return null;
        }

        else if (m_levelList.ContainsKey(levelIndex) == false)
        {
            Debug.Log("Levelindex is wrong");
            return null;
        }

        else
        {
            return m_levelList[levelIndex];
        }
    }

    public void ResetLevelList()
    {
        m_levelList.Clear();
        for(int i = m_minLevelIndex; i <= m_maxLevelIndex; i++)
        {
            LevelInfo tempLevelInfo = new LevelInfo();
            m_levelList.Add(i, tempLevelInfo);
        }
    }

    public int GetCurrentLevel()
    {
        return m_currentLevel;
    }
}
