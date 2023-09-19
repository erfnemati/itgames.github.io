using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace Assets.Scripts
{
    public class AlwaysOnNightsSceneManager : MonoBehaviour , TheSceneManager
    {
        [SerializeField] GameObject m_grayScreen;
        [SerializeField] GameObject m_resultMenu;
        [SerializeField] GameObject m_losingScreen;
        [SerializeField] GameObject m_winningScreen;
        [SerializeField] GameObject vfx;
        public void PauseGame()
        {
            Time.timeScale = 0.0f;
            AlwaysOnNigthsGameManager._instance.PauseGame();
        }
        public void ResumeGame()
        {
            Time.timeScale = 1.0f;
            AlwaysOnNigthsGameManager._instance.ResumeGame();
        }

        public void RestartGame()
        {
            int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentLevelIndex);
        }

        public void LoadNextLevel()
        {
            int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            int nextLevelIndex = (currentLevelIndex + 1) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(nextLevelIndex);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void ShowWinningScreen()
        {
            Debug.Log("You have won");
            m_grayScreen.gameObject.SetActive(true);
            m_resultMenu.gameObject.SetActive(true);
            m_winningScreen.gameObject.SetActive(true);

        }

        public void ShowLosingScreen()
        {
            Debug.Log("Losing");
            m_grayScreen.gameObject.SetActive(true);
            m_resultMenu.gameObject.SetActive(true);
            m_losingScreen.gameObject.SetActive(true);
            
        }
    }
}
