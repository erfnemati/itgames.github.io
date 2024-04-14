using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{

    private void Update()
    {
        if (Touchscreen.current != null)
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                ServiceLocator._instance.Get<BmsPlusSceneManager>().LoadNextLevel();
            }
        }

        else if (Input.anyKeyDown)
        {
            ServiceLocator._instance.Get<BmsPlusSceneManager>().LoadNextLevel();
        }
    }


    private void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = (currentLevelIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevelIndex);
    }
}
