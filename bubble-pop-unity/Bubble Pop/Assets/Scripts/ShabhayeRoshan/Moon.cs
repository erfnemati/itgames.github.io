using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Moon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float m_cycle;
    [SerializeField] float m_finalScale;
    [SerializeField] GameObject m_mainMenu;
    public void StartTransformation()
    {
        this.gameObject.transform.DOScale(m_finalScale, m_cycle).OnComplete(() => ActivateMainMenu()); ;
    }

    public void ActivateMainMenu()
    {
        m_mainMenu.SetActive(true);
    }

    public void LoadNextLevel()
    {
        int nextLevelIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevelIndex);
    }
}
