using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneName
{
    MainMenu,
    TutorialScene,
    levelScene,
    phoneScreen,
    ResualtScreen
}
public class BmsPlusSceneManager : MonoBehaviour, IGameService
{

    private int currentLevel=0;
    private List<LevelConfigData> levels;
    private PlayerLifeManager playerLifeManager;
    public LevelConfigData levelToLoad { get; private set; }
    public int NumberOfLevels { get { return levels.Count; } }
    private void Awake()
    {
        ServiceLocator._instance.Register(this,gameObject);
        //playerLifeManager = ServiceLocator._instance.Get<PlayerLifeManager>();

    }

    public void SetLevels(List<LevelConfigData> levels) => this.levels = levels;
    public int GetCurrentLevel() => currentLevel; 
    public void LoadNextLevel()
    {

        //playerLifeManager.ResetNumOfLives(); // this should be done in events OnLeveREatreat ...
        Debug.Log(levels.Count);
        levelToLoad = levels[++currentLevel];
        SceneManager.LoadScene((int)SceneName.levelScene);
    }
    public void RestartLevel()
    {
        ServiceLocator._instance.Get<SoundManager>().StopAllSoundEffects();
        levelToLoad = levels[currentLevel];
        SceneManager.LoadScene((int)SceneName.levelScene);
        
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public bool IsLastLvl()
    {
        Debug.Log("Checking");
        if (currentLevel == levels.Count)
        {
            Debug.Log("Last level");
            return true;
        }
        return false;
    }
    public void OnDestroy() { }

    public void LoadScene(SceneName sceneName)
    {
        ServiceLocator._instance.Get<SoundManager>().StopAllSoundEffects();
        SceneManager.LoadScene((int)sceneName);
    }
}
