using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.RoamingTutorialScripts
{
    class MainMenu : MonoBehaviour
    {
        [SerializeField] GameObject m_menuPanel;
        [SerializeField] float m_menuPanelFinalScale;
        [SerializeField] float m_menuPopCycle;
        private void Start()
        {
            m_menuPanel.SetActive(true);
            m_menuPanel.transform.DOScale(m_menuPanelFinalScale,m_menuPopCycle);
        }

        public void LoadNextLevel()
        {
            int nextLevelIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(nextLevelIndex);
        }
    }
}
