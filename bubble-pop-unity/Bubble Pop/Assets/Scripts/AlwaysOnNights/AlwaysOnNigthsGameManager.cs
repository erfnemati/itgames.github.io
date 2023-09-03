using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysOnNigthsGameManager : MonoBehaviour
{
    //Parameters for running game logic : 
    [SerializeField] AlwaysOnInputHandler m_inputHandler;
    public static AlwaysOnNigthsGameManager _instance;
    [SerializeField] float m_winingValue;
    [SerializeField] float m_losingValue;
    private float m_losingThreshold;

    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void CheckValue(float addedValue)
    {
        if (addedValue >= m_winingValue)
        {
            WinGame();
        }
        else if (addedValue < m_losingThreshold)
        {
            LoseGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("You have won");
    }

    private void LoseGame()
    {
        Debug.Log("You have lost");
    }

    public void PauseGame()
    {
        m_inputHandler.SetGamePauseState(true);
    }

    public void ResumeGame()
    {
        m_inputHandler.SetGamePauseState(false);
    }
}
