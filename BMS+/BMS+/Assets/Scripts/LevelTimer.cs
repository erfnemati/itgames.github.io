using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;
using System;
using GameData;
using System.Linq;


public class LevelTimer : MonoBehaviour , IGameService
{

    bool m_isLevelOver = false;
    bool m_isLevelNearOver = false;
    float offset = 0.25f;
    [SerializeField] float m_remainingTimer;
    [SerializeField] Slider m_timerSlider;
    [SerializeField] RTLTextMeshPro m_timerText;
    [SerializeField] AudioClip m_levelDefeatSoundEffect;
    [SerializeField] AudioClip m_nearLevelDefeatSoundEffect;

    private EventManager eventManager;

    private List<GameData.EventData> events;
    private void OnEnable()
    {
        eventManager.StartListening(EventName.OnLevelVictory, new Action(StopTimer));
        eventManager.StartListening(EventName.OnLevelDefeat, new Action(StopTimer));
        eventManager.StartListening(EventName.OnLevelRetreat, new Action(StopTimer));
        eventManager.StartListening(EventName.OnLevelRetreat, new Action(this.PlayLevelDefeatSound));
        eventManager.StartListening(EventName.OnLevelDefeat, new Action(this.PlayLevelDefeatSound));
        //Debug.Log("Level Timer enabling");
    }

    private void Awake()
    {
        ServiceLocator._instance.Register(this);
        eventManager = ServiceLocator._instance.Get<EventManager>();
    }
    // [q] have multiple events function acroos the project
    public void OnDestroy()
    {
        eventManager.StopListening(EventName.OnLevelVictory, new Action(StopTimer));
        eventManager.StopListening(EventName.OnLevelDefeat, new Action(StopTimer));
        eventManager.StopListening(EventName.OnLevelRetreat, new Action(StopTimer));
        eventManager.StopListening(EventName.OnLevelRetreat, new Action(this.PlayLevelDefeatSound));
        eventManager.StopListening(EventName.OnLevelDefeat, new Action(this.PlayLevelDefeatSound));
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
        CheckForBlitzEvent();
        if (m_remainingTimer < Mathf.Epsilon)
        {
            m_isLevelOver = true;
            //PlayerLifeManager._instance.DecrementNumOfLives();
            eventManager.TriggerEvent(EventName.OnTimeOver);
        }
        UpdateTimerUi();
    }

    private void CheckForBlitzEvent()
    {
        if(events != null)
        {
            EventData firstEvent = events.First();
            if(m_remainingTimer + offset > firstEvent.time && m_remainingTimer - offset < firstEvent.time)
            {
                eventManager.TriggerEvent<EventData>(EventName.OnBlitzHappened,firstEvent);
            }
        }
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

    public void SetBlitzModeInitials(List<EventData> events)
    {
        this.events = events;
    }
}
