using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BmsPlusSceneManager : MonoBehaviour
{
    public static BmsPlusSceneManager _instance;
    [SerializeField] int m_mainMenuIndex = 0;
    int m_lastLevelIndex = 54;
    public static int m_offsetFromStart = 2;
    public static int _numOfLevels = 25;


    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = (currentLevelIndex + 1) % SceneManager.sceneCountInBuildSettings;
        PlayerLifeManager._instance.ResetNumOfLives();
        SceneManager.LoadScene(nextLevelIndex);
    }

    public void RestartLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SoundManager._instance.StopAllSoundEffects();
        SceneManager.LoadScene(currentLevelIndex);
        
    }

    public void LoadMainMenu()
    {
        //Debug.Log("Loading main menu");
        if (SoundManager._instance != null)
        {
            SoundManager._instance.StopAllSoundEffects();
        }

        if (PlayerLifeManager._instance != null)
        {
            PlayerLifeManager._instance.ResetNumOfLives();
        }
        SceneManager.LoadScene(m_mainMenuIndex);
        
    }

    public void LoadPhoneScreenLevel()
    {
        SoundManager._instance.StopAllSoundEffects();
        PlayerLifeManager._instance.ResetNumOfLives();
        SceneManager.LoadScene(PhoneScreenManager._phoneScreenLevelName);
    }

    public int GetCurrentLevel()
    {
        int currentLevelBuildIndex = SceneManager.GetActiveScene().buildIndex;
        int level = currentLevelBuildIndex - m_offsetFromStart + 1;
        return level;
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public bool IsLastLvl()
    {
        Debug.Log("Checking");
        if (SceneManager.GetActiveScene().buildIndex == m_lastLevelIndex)
        {
            Debug.Log("Last level");
            return true;
        }
        return false;
    }
}
