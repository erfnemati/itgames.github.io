using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StartScreen : MonoBehaviour
{
    
    private void Update()
    {
        if (Input.touchCount != 0)
        {
            LoadStartMenu();
        }
    }
    private void  LoadStartMenu()
    {
        Debug.Log("Loading");
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = (currentLevelIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevelIndex);
    }
}
