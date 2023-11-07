using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager _levelSelectManagerInstance;
    private Dictionary<int, LevelInfo> m_levelList = new Dictionary<int, LevelInfo>();
    [SerializeField] int m_maxLevelIndex;
    [SerializeField] int m_minLevelIndex;

    // Start is called before the first frame update
    void Start()
    {
        if (_levelSelectManagerInstance != null && _levelSelectManagerInstance != this)
        {
            _levelSelectManagerInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
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
    }
}
