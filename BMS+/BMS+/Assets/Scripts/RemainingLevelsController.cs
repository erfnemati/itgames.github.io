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
        int numOfLevels = BmsPlusSceneManager._numOfLevels;
        int currentLevel = BmsPlusSceneManager._instance.GetCurrentLevel();

        m_remainingText.text = currentLevel + " مرحله" + " از " + numOfLevels + " تا رو حل کردی!";
    }
}
