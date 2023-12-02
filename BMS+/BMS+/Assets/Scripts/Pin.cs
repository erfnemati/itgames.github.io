using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;
using UnityEngine.UI;

public class Pin : MonoBehaviour
{
    [SerializeField] PinColor m_pinColor;
    [SerializeField] int m_numOfUsages;

    //UI code here:
    [SerializeField] RTLTextMeshPro m_numOfUsagesText;
    [SerializeField] GameObject m_buttonFrame;

    public Pin(PinColor pinColor)
    {
        m_pinColor = pinColor;
    }

    public PinColor GetPinColor()
    {
        return m_pinColor;
    }

    public void IncrementUsages()
    {
        m_numOfUsages++;
        UpdateUi();
    }

    public void DecrementUsages()
    {
        m_numOfUsages--;
        if(m_numOfUsages < 0)
        {
            m_numOfUsages = 0;
        }
        UpdateUi();
    }

    //UI code here:
    public void UpdateUi()
    {
        m_numOfUsagesText.text = m_numOfUsages + "";
    }

    public void ResetPinUi()
    {
        m_buttonFrame.gameObject.SetActive(false);
    }

    
}
