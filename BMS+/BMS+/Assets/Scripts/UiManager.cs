using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject m_blurredBackground;
    [SerializeField] GameObject m_VictoryScreen;
    [SerializeField] GameObject m_DefeatScreen;
    [SerializeField] float m_scalingTime;

    private void OnEnable()
    {
        LevelManager.OnLevelVictory += InvokeVictoryScreen;
        LevelManager.OnLevelDefeat += ShowDefeatScreen;
        Debug.Log("Ui manager enabling");
    }

    private void OnDisable()
    {
        LevelManager.OnLevelDefeat -= ShowDefeatScreen;
        LevelManager.OnLevelVictory -= InvokeVictoryScreen;
        Debug.Log("Ui manager disabling");
    }

    private void InvokeVictoryScreen()
    {
        Debug.Log("Show winning screen");
        Invoke(nameof(ShowVictoryScreen), 0.5f);
        

    }

    private void ShowVictoryScreen()
    {
        m_blurredBackground.gameObject.SetActive(true);
        AnimateScreen(m_VictoryScreen);
    }

    private void AnimateScreen(GameObject targetGameObject)
    {
        targetGameObject.gameObject.SetActive(true);
        Vector3 initialScale = targetGameObject.transform.localScale;
        targetGameObject.transform.localScale = Vector3.zero;
        targetGameObject.transform.DOScale(initialScale, m_scalingTime).SetEase(Ease.InOutCubic);

    }


    private void ShowDefeatScreen()
    {
        m_blurredBackground.SetActive(true);
        m_blurredBackground.gameObject.SetActive(true);
        AnimateScreen(m_DefeatScreen);

    }
}
