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
    [SerializeField] RTLTextMeshPro m_numOfUsagesText;
    [SerializeField] GameObject m_buttonFrame;

    public void InitializePin( Color ConfigPinColor,int ConfigNumOfUsages)
    {
        m_pinColor = ConfigPinColor;
        m_numOfUsages=ConfigNumOfUsages;
    }
    private void Start()
    {
        SetSprite();
        UpdateTextOfPin();
    }
    private void SetSprite()
    {
        gameObject.GetComponentInChildren<Image>().sprite = DataManager._instance.GetColorData<PinColorData>(m_pinColor).sprite; //[E] : bugkhiz
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
