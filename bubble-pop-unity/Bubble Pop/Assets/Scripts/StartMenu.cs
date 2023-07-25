using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public static StartMenu m_startMenuInstance;
    public Slider m_volumeSlider;
    [SerializeField] 
    // Start is called before the first frame update
    void Start()
    {
        if (m_startMenuInstance == null)
        {
            m_startMenuInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayGame()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = (currentLevel + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevelIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AdjustValume()
    {
        float volume = m_volumeSlider.value;
    }

    
}
