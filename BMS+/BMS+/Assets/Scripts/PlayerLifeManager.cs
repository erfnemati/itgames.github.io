using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [q] how to handle this small classes
public class PlayerLifeManager : IGameService
{
    public static PlayerLifeManager _instance;

    public delegate void GameOver();
    public static event GameOver GameIsOver;

    [SerializeField] int m_currentNumOfLives = 3;// get this from config
    [SerializeField] int m_initialNumberOfLives;

    public PlayerLifeManager()
    {
        ServiceLocator.Current.Register(this);
        InitializeVariables();
    }
    public void InitializeVariables()
    {
        //m_currentNumOfLives = 

    }
    public void PreDestroy()
    {

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
