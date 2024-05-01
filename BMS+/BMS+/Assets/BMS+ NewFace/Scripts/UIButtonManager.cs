using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonManager : MonoBehaviour
{
    BmsPlusSceneManager sceneManager;
    PlayerLifeManager playerLifeManager;
    PersistentDataManager persistentDataManager;
    EventManager eventManager;
    private void Awake()
    {
        sceneManager = ServiceLocator._instance.Get<BmsPlusSceneManager>();
        playerLifeManager= ServiceLocator._instance.Get<PlayerLifeManager>();
        persistentDataManager = ServiceLocator._instance.Get<PersistentDataManager>();
        eventManager = ServiceLocator._instance.Get<EventManager>();
    }
    public void LoadMainMenuTopBar() => eventManager.TriggerEvent(EventName.OnLevelRetreat);
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
            eventManager.TriggerEvent(EventName.OnLevelRetreat);
        else
            eventManager.TriggerEvent(EventName.OnLevelDefeat);


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
    //public void TopBarRetryLevel()
    //{
    //    playerLifeManager.DecrementNumOfLives();
    //    persistentDataManager.GetLevel();
    //    persistentDataManager.IncrementNumOfConsumedLives();
    //    sceneManager.RestartLevel();
    //}
}
