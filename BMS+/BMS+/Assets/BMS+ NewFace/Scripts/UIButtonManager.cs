using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonManager : MonoBehaviour
{
    BmsPlusSceneManager sceneManager;
    PlayerLifeManager playerLifeManager;
    private void Awake()
    {
        sceneManager = ServiceLocator._instance.Get<BmsPlusSceneManager>();
        playerLifeManager= ServiceLocator._instance.Get<PlayerLifeManager>();
    }
    public void LoadMainMenue()
    {
        playerLifeManager.ResetNumOfLives();
        sceneManager.LoadPhoneScreenLevel();
    }
    public void RetryAfterWinLevel()
    {
        playerLifeManager.ResetNumOfLives();
        sceneManager.RestartLevel();
    }
    public void RetryAfterLooseLevel()
    {
        playerLifeManager.DecrementNumOfLives();
        sceneManager.RestartLevel();
    }
    public void LoadNextLevel()
    {
        playerLifeManager.ResetNumOfLives();
        sceneManager.LoadNextLevel();
    }
}
