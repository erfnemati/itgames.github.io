using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;

public class LevelTimer : MonoBehaviour
{
    public delegate void TimeOverAction();
    public static event TimeOverAction OnTimeOver;
    bool m_isLevelOver = false;

    [SerializeField] float m_remainingTimer;
    [SerializeField] Slider m_timerSlider;
    [SerializeField] RTLTextMeshPro m_timerText;


    private void Start()
    {
        m_timerSlider.maxValue = m_remainingTimer;
        UpdateTimerUi();
    }

    void Update()
    {
        if (m_isLevelOver == false)
        {
            UpdateTimer();
        }
        
    }

    
    private void UpdateTimer()
    {
        m_remainingTimer -= Time.deltaTime;
        if (m_remainingTimer < Mathf.Epsilon)
        {
            m_isLevelOver = true;
            OnTimeOver();
        }
        UpdateTimerUi();
    }

    
    private void UpdateTimerUi()
    {
        UpdateTimerSlider();
        UpdateTimerText();
    }
    private void UpdateTimerSlider()
    {
        m_timerSlider.value = m_remainingTimer;
    }

    private void UpdateTimerText()
    {
        if (m_remainingTimer - 60.0f < Mathf.Epsilon)
        {
            m_timerText.text = (int)m_remainingTimer + "";
        }
        else
        {
            int minutes = (int)m_remainingTimer / 60;
            int seconds = (int)m_remainingTimer - (minutes * 60);
            if (seconds < 10)
            {
                m_timerText.text = "0" + seconds + ":" + minutes;               
            }
            else
            {
                m_timerText.text = seconds + ":" + minutes;
            }
            
        }
    }
}
