using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonManager : MonoBehaviour
{
    BmsPlusSceneManager sceneManager;
    PlayerLifeManager playerLifeManager;
    PersistentDataManager persistentDataManager;
    private void Awake()
    {
        sceneManager = ServiceLocator._instance.Get<BmsPlusSceneManager>();
        playerLifeManager= ServiceLocator._instance.Get<PlayerLifeManager>();
        persistentDataManager = ServiceLocator._instance.Get<PersistentDataManager>();
    }
    public void LoadMainMenue()
    {
        playerLifeManager.ResetNumOfLives();
        sceneManager.LoadScene(SceneName.phoneScreen);
    }
    public void RetryAfterWinLevel()
    {
        playerLifeManager.ResetNumOfLives();
        sceneManager.RestartLevel();
    }
    public void RetryTopBar()
    {
        if (playerLifeManager.GetCurrentNumberOfLives() == 1)
            ServiceLocator._instance.Get<EventManager>().TriggerEvent(EventName.OnLevelRetreat);
        else
            ServiceLocator._instance.Get<EventManager>().TriggerEvent(EventName.OnLevelDefeat);


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
    public void TopBarRetryLevel()
    {
        playerLifeManager.DecrementNumOfLives();
        persistentDataManager.GetLevel();
        persistentDataManager.IncrementNumOfConsumedLives();
        sceneManager.RestartLevel();
    }
}
