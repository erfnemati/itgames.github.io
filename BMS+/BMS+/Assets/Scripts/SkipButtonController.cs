using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButtonController : MonoBehaviour
{
    private int m_waitForExit = 5;
    private int m_numOfTries = 0;

    public void Quit()
    {
        if (m_numOfTries >= m_waitForExit)
        {
            Debug.Log("Quiting");
            Application.Quit();
        }
        m_numOfTries++;
       
    }

    public void SkipLevel()
    {
        if (m_numOfTries  >= m_waitForExit)
        {
            Debug.Log("Skipping");
            ServiceLocator._instance.Get<PlayerLifeManager>().ResetNumOfLives();
            ServiceLocator._instance.Get<BmsPlusSceneManager>().LoadNextLevel();
        }
        m_numOfTries++;
    }
}
