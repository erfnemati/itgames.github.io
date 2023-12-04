using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player _instance;
    Pin m_currentPlayerPin;
    [SerializeField] GameObject m_redPin;
    [SerializeField] GameObject m_yellowPin;
    [SerializeField] GameObject m_bluePin;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public Pin GetPlayerPin()
    {
        if (m_currentPlayerPin.isPinLeft())
        {
            return m_currentPlayerPin;
        }
        return null;
        
    }

    //Probably not the best idea:

    public void PickRedPin()
    {
        if (m_currentPlayerPin != null)
        {
            m_currentPlayerPin.ResetPinUi();
        }
        m_currentPlayerPin = m_redPin.GetComponent<Pin>();
    }

    public void PickYellowPin()
    {
        if (m_currentPlayerPin != null)
        {
            m_currentPlayerPin.ResetPinUi();
        }
        m_currentPlayerPin = m_yellowPin.GetComponent<Pin>();
    }

    public void PickBluePin()
    {
        if (m_currentPlayerPin != null)
        {
            m_currentPlayerPin.ResetPinUi();
        }
        m_currentPlayerPin = m_bluePin.GetComponent<Pin>();
    }

    public void ReleasePin()
    {
        m_currentPlayerPin.DecrementUsages();
    }

    public void ReloadPin(Pin pin)
    {
        pin.IncrementUsages();
    }
}
