using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;
using UnityEngine.UI;
using System;
using GameEnums;
using ConfigData;

public class Pin1 : MonoBehaviour
{
    VectorInt m_pinColor;
    PinConfigData m_pinConfig;
    public int m_numOfUsages { get; private set; } //[C]: whats the difference between tower pins and station pins?

    //UI code here:
    [SerializeField] Button m_button;
    [SerializeField] RTLTextMeshPro m_numOfUsagesText;
    [SerializeField] GameObject m_buttonFrame;
    private EventManager eventManager;
    public void InitializePin(VectorInt ConfigPinColor,int ConfigNumOfUsages)
    {
        m_pinColor = ConfigPinColor;
        m_numOfUsages=ConfigNumOfUsages;
        m_pinConfig = ServiceLocator._instance.Get<DataManager>().GetData<PinConfigData>(m_pinColor);
    }
    private void Awake()
    {
        eventManager  = ServiceLocator._instance.Get<EventManager>();


    }
    public void OnDisable()
    {
        eventManager.StartListening(EventName.OnLevelDefeat, new Action(DisableButton));
        eventManager.StartListening(EventName.OnLevelRetreat, new Action(DisableButton));
        eventManager.StartListening(EventName.OnLevelVictory, new Action(DisableButton));
       // eventManager.StartListening(EventName.OnBlitzHappened, new Action<PinName>(BlitzEventIncrementUsage));
    }
    public void OnEnable()
    {
        eventManager.StopListening(EventName.OnLevelDefeat, new Action(DisableButton));
        eventManager.StopListening(EventName.OnLevelRetreat, new Action(DisableButton));
        eventManager.StopListening(EventName.OnLevelVictory, new Action(DisableButton));
        //eventManager.StopListening(EventName.OnBlitzHappened, new Action<PinName>(BlitzEventIncrementUsage));

    }
    private void Start()
    {
        SetSprite();
        UpdateTextOfPin();
    }
    private void SetSprite()
    {
        gameObject.GetComponentInChildren<Image>().sprite = ServiceLocator._instance.Get<DataManager>().GetData<ConfigData.PinConfigData>(m_pinColor).sprite; //[E] : bugkhiz
    }
    private void DisableButton()
    {
        gameObject.GetComponentInChildren<Button>().enabled = false;
    }


    public VectorInt GetPinColor()
    {
        return m_pinColor;
    }

    public void IncrementUsages()
    {
        m_numOfUsages++;
        UpdateTextOfPin();
    }

    public void DecrementUsages()
    {
        m_numOfUsages--;
        if(m_numOfUsages < 0)
        {
            m_numOfUsages = 0;
        }
        UpdateTextOfPin();
    }

    //UI code here:
    public void UpdateTextOfPin()
    {
        m_numOfUsagesText.text = m_numOfUsages + "";
    }

    public void ResetPinUi()
    {
        gameObject.GetComponentsInChildren<Image>()[2].gameObject.SetActive(false);
    }

    public bool isPinLeft()
    {
        if (m_numOfUsages >= 1)
            return true;
        return false;
    }

    private void BlitzEventIncrementUsage(PinName name)
    {
        if(name==m_pinConfig.name)
            IncrementUsages();
    }
    
}
