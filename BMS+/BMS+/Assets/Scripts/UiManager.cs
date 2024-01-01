using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject m_blurredBackground;
    [SerializeField] GameObject m_VictoryScreen;
    [SerializeField] GameObject m_defeatScreen;
    [SerializeField] GameObject m_gameOverScreen;
    [SerializeField] float m_scalingTime;
    [SerializeField] GameObject m_firstHeart;
    [SerializeField] GameObject m_secHeart;
    [SerializeField] GameObject m_thirdHeart;


    private void OnEnable()
    {
        LevelManager.OnLevelVictory += InvokeVictoryScreen;
        LevelManager.OnLevelDefeat += ShowDefeatScreen;
        LevelManager.OnLevelRetreat += ShowRetreatScreen;
        //Debug.Log("Ui manager enabling");
    }

    private void OnDisable()
    {
        LevelManager.OnLevelDefeat -= ShowDefeatScreen;
        LevelManager.OnLevelVictory -= InvokeVictoryScreen;
        LevelManager.OnLevelRetreat -= ShowRetreatScreen;
        //Debug.Log("Ui manager disabling");
    }

    private void Start()
    {
        UpdateNumberOfLives();
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
        int m_numberOfLives = PlayerLifeManager._instance.GetCurrentNumberOfLives();
        m_blurredBackground.gameObject.SetActive(true);
        if (m_numberOfLives < 1)
        {
            AnimateScreen(m_gameOverScreen);
        }
        else
        {
            AnimateScreen(m_defeatScreen);
        }
        
    }

    private void ShowRetreatScreen()
    {
        m_blurredBackground.gameObject.SetActive(true);
        AnimateScreen(m_gameOverScreen);
    }

    private void UpdateNumberOfLives()
    {
        if (PlayerLifeManager._instance == null)
        {
            return;
        }

        int m_numberOfLives = PlayerLifeManager._instance.GetCurrentNumberOfLives();

        if (m_numberOfLives == 1)
        {
            m_firstHeart.gameObject.SetActive(true);
            m_secHeart.gameObject.SetActive(false);
            m_thirdHeart.gameObject.SetActive(false);
        }
        else if (m_numberOfLives == 2)
        {
            m_firstHeart.gameObject.SetActive(true);
            m_secHeart.gameObject.SetActive(true);
            m_thirdHeart.gameObject.SetActive(false);
        }
        else if (m_numberOfLives == 3)
        {
            m_firstHeart.gameObject.SetActive(true);
            m_secHeart.gameObject.SetActive(true);
            m_thirdHeart.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Something is wrong");
        }
    }
}
