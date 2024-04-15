using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BmsPlusSceneManager : MonoBehaviour, IGameService
{
    public enum SceneName
    {
        MainMenu,
        //Tutorial,
        levelScene,
        phoneScreen,
        ResualtScreen
    }
    private int currentLevel=-1;
    private List<LevelConfigData> levels;
    private PlayerLifeManager playerLifeManager;
    public LevelConfigData levelToLoad { get; private set; }
    public int NumberOfLevels { get { return levels.Count; } }
    private void Awake()
    {
        ServiceLocator._instance.Register(this,gameObject);
        //playerLifeManager = ServiceLocator._instance.Get<PlayerLifeManager>();

    }
    private void Start()
    {
        levels = ServiceLocator._instance.Get<DataManager>().GetData<LevelsConfig>().levels;

    }
    public void LoadNextLevel()
    {

        //playerLifeManager.ResetNumOfLives(); // this should be done in events OnLeveREatreat ...
        levelToLoad = levels[++currentLevel];
        SceneManager.LoadScene((int)SceneName.levelScene);
    }
  //  public void LoadTutorial() => SceneManager.LoadScene((int)SceneName.Tutorial);

    public void RestartLevel()
    {
        SoundManager._instance.StopAllSoundEffects();
        levelToLoad = levels[currentLevel];
        SceneManager.LoadScene((int)SceneName.levelScene);
        
    }
    public void LoadMainMenu()
    {
        //Debug.Log("Loading main menu");
        if (SoundManager._instance != null)
        {
            SoundManager._instance.StopAllSoundEffects();
        }

        //if (playerLifeManager != null)
        //{
        //    playerLifeManager.ResetNumOfLives();
        //}
        SceneManager.LoadScene((int)SceneName.MainMenu);
        
    }

    public void LoadPhoneScreenLevel()
    {
        SoundManager._instance.StopAllSoundEffects();
        //playerLifeManager.ResetNumOfLives();
        SceneManager.LoadScene((int)SceneName.phoneScreen);
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
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

    public void LoeadRsualtScreen()
    {
        SceneManager.LoadScene((int)SceneName.ResualtScreen);
    }
}
