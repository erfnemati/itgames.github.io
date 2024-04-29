using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;
using System;
using GameData;
using System.Linq;
using ConfigData;
using GameEnums;


public class LevelTimer : MonoBehaviour , IGameService
{

    bool m_isLevelOver = false;
    bool m_isLevelNearOver = false;
    float offset = 0.25f;
    float levelTime;
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
        eventManager.StartListening(EventName.OnLevelVictory, new Action(SetGameDuration));
        eventManager.StartListening(EventName.OnLevelDefeat, new Action(SetGameDuration));
        eventManager.StartListening(EventName.OnLevelRetreat, new Action(SetGameDuration));

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

        eventManager.StopListening(EventName.OnLevelVictory, new Action(SetGameDuration));
        eventManager.StopListening(EventName.OnLevelDefeat, new Action(SetGameDuration));
        eventManager.StopListening(EventName.OnLevelRetreat, new Action(SetGameDuration));
        ServiceLocator._instance.Unregister<LevelTimer>();
    }
    private void Start()
    {
        m_remainingTimer = levelTime;
        m_timerSlider.maxValue = levelTime;
        InittializeVariables();
        UpdateTimerUi();
    }
    public void SetlevelTime(float levelTime) => this.levelTime = levelTime;

    private void InittializeVariables()
    {
        m_levelDefeatSoundEffect = ServiceLocator._instance.Get<DataManager>().GetData<SoundConfigData>((int)SoundName.levelDefeatSoundEffect).audioClip;
        m_nearLevelDefeatSoundEffect = ServiceLocator._instance.Get<DataManager>().GetData<SoundConfigData>((int)SoundName.nearLevelDefeatSoundEffect).audioClip;

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
        if(events.Count > 0)
        {
            EventData firstEvent = events.First();
            if(m_remainingTimer + offset > firstEvent.time && m_remainingTimer - offset < firstEvent.time)
            {
                eventManager.TriggerEvent<EventData>(EventName.OnBlitzHappened,firstEvent);
                events.Remove(firstEvent);
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
            ServiceLocator._instance.Get<SoundManager>().PlaySoundEffect(m_nearLevelDefeatSoundEffect);
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
    private void SetGameDuration() => ServiceLocator._instance.Get<PersistentDataManager>().GetCurrentPlayerData().UpdateTime(m_timerSlider.maxValue-m_remainingTimer);

    private void PlayLevelDefeatSound()
    {
        ServiceLocator._instance.Get<SoundManager>().PlaySoundEffect(m_levelDefeatSoundEffect);
    }

    public void SetBlitzModeInitials(List<EventData> events)
    {
        this.events = events;
    }
}
