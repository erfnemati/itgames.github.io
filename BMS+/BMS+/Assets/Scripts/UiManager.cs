using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

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
    [SerializeField] GameObject m_oneThirdAntenna;
    [SerializeField] GameObject m_twoThirdAntenna;
    [SerializeField] GameObject m_CompleteAntenna;

    private EventManager eventManager;
    private void OnEnable()
    {
        eventManager.StartListening(EventName.OnLevelVictory, new Action(this.InvokeVictoryScreen));
        eventManager.StartListening(EventName.OnLevelDefeat, new Action(this.ShowDefeatScreen));
        eventManager.StartListening(EventName.OnLevelRetreat, new Action(this.ShowRetreatScreen));
        //Debug.Log("Ui manager enabling");
    }

    private void OnDisable()
    {
        eventManager.StopListening(EventName.OnLevelVictory, new Action(this.InvokeVictoryScreen));
        eventManager.StopListening(EventName.OnLevelDefeat, new Action(this.ShowDefeatScreen));
        eventManager.StopListening(EventName.OnLevelRetreat, new Action(this.ShowRetreatScreen));
        //Debug.Log("Ui manager disabling");
    }

    private void Awake()
    {
        eventManager = ServiceLocator._instance.Get<EventManager>();
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
        int m_numberOfLives = ServiceLocator._instance.Get<PlayerLifeManager>().GetCurrentNumberOfLives();
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

        int m_numberOfLives = ServiceLocator._instance.Get<PlayerLifeManager>().GetCurrentNumberOfLives();

        if (m_numberOfLives == 1)
        {
            m_oneThirdAntenna.gameObject.SetActive(true);
            m_twoThirdAntenna.gameObject.SetActive(false);
            m_CompleteAntenna.gameObject.SetActive(false);

        }
        else if (m_numberOfLives == 2)
        {
            m_oneThirdAntenna.gameObject.SetActive(false);
            m_twoThirdAntenna.gameObject.SetActive(true);
            m_CompleteAntenna.gameObject.SetActive(false);
        }
        else if (m_numberOfLives == 3)
        {
            m_oneThirdAntenna.gameObject.SetActive(false);
            m_twoThirdAntenna.gameObject.SetActive(false);
            m_CompleteAntenna.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Something is wrong");
        }
    }
}
