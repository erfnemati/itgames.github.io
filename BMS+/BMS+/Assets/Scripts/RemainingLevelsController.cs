using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;

public class RemainingLevelsController : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro m_remainingText;

    private void Start()
    {
        UpdateRemainingText();
    }

    private void UpdateRemainingText()
    {
        
        int numOfLevels = ServiceLocator._instance.Get<BmsPlusSceneManager>().NumberOfLevels;
        int currentLevel = ServiceLocator._instance.Get<BmsPlusSceneManager>().GetCurrentLevel();

        m_remainingText.text = currentLevel + " مرحله" + " از " + numOfLevels + " تا رو حل کردی!";
    }
}
