using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverlayUiController : MonoBehaviour
{
    public static OverlayUiController _instance;

    [SerializeField] GameObject m_grayScreen;
    [SerializeField] GameObject m_resultMenu;

    private void Start()
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
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        RoamingInputHandler._instance.SetIsGamePuased(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        RoamingInputHandler._instance.SetIsGamePuased(true);
    }

    public void RestartGame()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }

    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }


    public void ShowResultMenu()
    {

    }

    


}
