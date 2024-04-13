using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;
using UnityEngine.UI;

public class Pin : MonoBehaviour
{
    [SerializeField] PinColor m_pinColor;
    [SerializeField] int m_numOfUsages; //[C]: whats the difference between tower pins and station pins?

    //UI code here:
    [SerializeField] RTLTextMeshPro m_numOfUsagesText;
    [SerializeField] GameObject m_buttonFrame;


    private void Start()
    {
        UpdateTextOfPin();
    }
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
