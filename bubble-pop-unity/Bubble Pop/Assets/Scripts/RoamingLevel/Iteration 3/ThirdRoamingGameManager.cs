using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdRoamingGameManager : MonoBehaviour
{
    [SerializeField] ShuttlePartGenerator m_shuttlePartGenerator;

    private void Start()
    {
        GoNextState();
    }
    public void EndLevel()
    {
        Debug.Log("Level is ended");
    }

    public void GoNextState()
    {
        m_shuttlePartGenerator.GenerateShuttlePart();
    }

    public void RestartState()
    {
        m_shuttlePartGenerator.ReGenerateShuttlePart();
    }

    
}
