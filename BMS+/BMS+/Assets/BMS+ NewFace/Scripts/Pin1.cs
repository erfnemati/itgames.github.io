using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;
using UnityEngine.UI;

public class Pin1 : MonoBehaviour
{
    [SerializeField] Color m_pinColor;
    [SerializeField] int m_numOfUsages; //[C]: whats the difference between tower pins and station pins?

    //UI code here:
    [SerializeField] Button m_button;
    [SerializeField] RTLTextMeshPro m_numOfUsagesText;
    [SerializeField] GameObject m_buttonFrame;
    private LevelManager1 m_levelManager;
    public void InitializePin( Color ConfigPinColor,int ConfigNumOfUsages)
    {
        m_pinColor = ConfigPinColor;
        m_numOfUsages=ConfigNumOfUsages;
    }
    private void Awake()
    {
        m_levelManager = ServiceLocator.Current.Get<LevelManager1>();

    }
    public void OnDisable()
    {
        m_levelManager.OnLevelDefeat -= DisableButton;
        m_levelManager.OnLevelRetreat -= DisableButton;
        m_levelManager.OnLevelVictory -= DisableButton;
    }
    public void OnEnable()
    {
        m_levelManager.OnLevelDefeat += DisableButton;
        m_levelManager.OnLevelRetreat += DisableButton;
        m_levelManager.OnLevelVictory += DisableButton;
    }
    private void Start()
    {
        SetSprite();
        UpdateTextOfPin();
    }
    private void SetSprite()
    {
        gameObject.GetComponentInChildren<Image>().sprite = DataManager._instance.GetData<PinColorData>(m_pinColor).sprite; //[E] : bugkhiz
    }
    private void DisableButton()
    {
        gameObject.GetComponentInChildren<Button>().enabled = false;
    }


    public Color GetPinColor()
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
        m_buttonFrame.gameObject.SetActive(false);
    }

    public bool isPinLeft()
    {
        if (m_numOfUsages >= 1)
            return true;
        return false;
    }
    
}
