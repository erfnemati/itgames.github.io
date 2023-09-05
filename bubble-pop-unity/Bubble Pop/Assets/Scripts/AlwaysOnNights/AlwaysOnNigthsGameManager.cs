using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysOnNigthsGameManager : MonoBehaviour
{
    //Parameters for running game logic : 
    [SerializeField] AlwaysOnInputHandler m_inputHandler;
    public static AlwaysOnNigthsGameManager _instance;
    [SerializeField] float m_moonGeneratonThreshold = 10.0f;
    [SerializeField] float m_losingValue;
    private float m_losingThreshold = 1f;

    //Parameters for generating moon : 
    [SerializeField] List<Transform> m_moonSpots = new List<Transform>();
    [SerializeField] GameObject m_moonObject;
    private bool m_isMoonGenerated = false;

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
        if (addedValue >= m_moonGeneratonThreshold)
        {
            if (m_isMoonGenerated == false)
            {
                m_isMoonGenerated = true;
                GenerateMoonBubble();
            }
        }
        else if (addedValue < m_losingThreshold)
        {
            LoseGame();
        }
    }

    private void GenerateMoonBubble()
    {
        int randomIndex = Random.Range(0, m_moonSpots.Count);
        GameObject tempObject = Instantiate(m_moonObject, m_moonSpots[randomIndex].position, Quaternion.identity);
        Debug.Log("Moon generated");
    }

    private void LoseGame()
    {
        Debug.Log("You have lost");
        m_inputHandler.FinishGame();
        Time.timeScale = 0.0f;
        
    }

    public void WinGame()
    {
        m_inputHandler.FinishGame();
        Time.timeScale = 0.0f;
    }

    public void PauseGame()
    {
        m_inputHandler.SetGamePauseState(true);
    }

    public void ResumeGame()
    {
        m_inputHandler.SetGamePauseState(false);
    }

    public void SetIsMoonGenerated(bool isMoonGenerated)
    {
        m_isMoonGenerated = isMoonGenerated;
    }
}
