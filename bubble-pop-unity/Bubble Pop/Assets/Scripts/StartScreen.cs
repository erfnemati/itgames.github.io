using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScreen : MonoBehaviour
{
    [SerializeField] float m_delay;
    private void Start()
    {
        Invoke(nameof(LoadStartMenu), m_delay);
    }
    private void  LoadStartMenu()
    {
        Debug.Log("Loading");
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = (currentLevelIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevelIndex);
    }
}
