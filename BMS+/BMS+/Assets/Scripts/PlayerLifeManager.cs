using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    public static PlayerLifeManager _instance;

    public delegate void GameOver();
    public static event GameOver GameIsOver;

    [SerializeField] int m_currentNumOfLives = 3;
    [SerializeField] int m_initialNumberOfLives;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Start()
    {
        m_currentNumOfLives = m_initialNumberOfLives;
    }

    public void ResetNumOfLives()
    {
        m_currentNumOfLives = m_initialNumberOfLives;
    }

    public int GetCurrentNumberOfLives()
    {
        return m_currentNumOfLives;
    }

    public void DecrementNumOfLives()
    {
        Debug.Log("Decrementing");
        m_currentNumOfLives -= 1;
        if (m_currentNumOfLives <=0)
        {
            m_currentNumOfLives = 0;
            if (PersistentDataManager._instance != null)
            {
                GameIsOver();
            }
        }
    }
    
    public void SetNumOfLives(int numOfLives)
    {
        m_currentNumOfLives = numOfLives;
    }
}
