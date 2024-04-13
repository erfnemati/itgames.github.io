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
    bool m_isLevelNearOver = false;

    [SerializeField] float m_remainingTimer;
    [SerializeField] Slider m_timerSlider;
    [SerializeField] RTLTextMeshPro m_timerText;
    [SerializeField] AudioClip m_levelDefeatSoundEffect;
    [SerializeField] AudioClip m_nearLevelDefeatSoundEffect;

    private void OnEnable()
    {
        LevelManager.OnLevelVictory += StopTimer;
        LevelManager.OnLevelDefeat += StopTimer;
        LevelManager.OnLevelDefeat += PlayLevelDefeatSound;
        LevelManager.OnLevelRetreat += PlayLevelDefeatSound;
        LevelManager.OnLevelRetreat += StopTimer;
        //Debug.Log("Level Timer enabling");
    }

  // [q] have multiple events function acroos the project
    private void OnDisable()
    {
        LevelManager.OnLevelVictory -= StopTimer;
        LevelManager.OnLevelDefeat -= StopTimer;
        LevelManager.OnLevelDefeat -= PlayLevelDefeatSound;
        LevelManager.OnLevelRetreat -= PlayLevelDefeatSound;
        LevelManager.OnLevelRetreat -= StopTimer;
        //Debug.Log("Level Timer disabling");
    }
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
            //PlayerLifeManager._instance.DecrementNumOfLives();
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
        if (m_remainingTimer - 5f < Mathf.Epsilon && m_isLevelNearOver == false)
        {

            //SoundManager._instance.FadeBackgroundMusic();
            SoundManager._instance.PlaySoundEffect(m_nearLevelDefeatSoundEffect);
            m_isLevelNearOver = true;
        }
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

    private void StopTimer()
    {
        m_isLevelOver = true;
    }

    private void PlayLevelDefeatSound()
    {
        SoundManager._instance.PlaySoundEffect(m_levelDefeatSoundEffect);
    }

    
}
