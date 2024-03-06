using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player _instance;
    [SerializeField] Pin m_currentPlayerPin;
    private bool m_isLevelOver = false;
    [SerializeField] GameObject m_redPin;
    [SerializeField] GameObject m_yellowPin;
    [SerializeField] GameObject m_bluePin;

    [SerializeField] AudioClip m_playerChoosingSound;
    

    private void OnEnable()
    {
        LevelManager.OnLevelDefeat += LevelIsOver;
        LevelManager.OnLevelRetreat += LevelIsOver;
        LevelManager.OnLevelVictory += LevelIsOver;
        //Debug.Log("Player enabling");
    }

    private void OnDisable()
    {
        LevelManager.OnLevelDefeat -= LevelIsOver;
        LevelManager.OnLevelRetreat -= LevelIsOver;
        LevelManager.OnLevelVictory -= LevelIsOver;
        //Debug.Log("Player Disabling");
    }
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
        if (m_currentPlayerPin == null)
        {
            return null;
        }

        if (m_currentPlayerPin.isPinLeft())
        {
            return m_currentPlayerPin;
        }
        return null;
        
    }

    //Probably not the best idea:

    public void PickRedPin()
    {
        if(m_isLevelOver)
        {
            return;
        }
        if (m_currentPlayerPin != null)
        {
            m_currentPlayerPin.ResetPinUi();
        }
        m_currentPlayerPin = m_redPin.GetComponent<Pin>();
        SoundManager._instance.PlaySoundEffect(m_playerChoosingSound);
    }

    public void PickYellowPin()
    {
        if (m_isLevelOver)
        {
            return;
        }
        if (m_currentPlayerPin != null)
        {
            m_currentPlayerPin.ResetPinUi();
        }
        m_currentPlayerPin = m_yellowPin.GetComponent<Pin>();
        SoundManager._instance.PlaySoundEffect(m_playerChoosingSound);
    }

    public void PickBluePin()
    {
        if (m_isLevelOver)
        {
            return;
        }
        if (m_currentPlayerPin != null)
        {
            m_currentPlayerPin.ResetPinUi();
        }
        m_currentPlayerPin = m_bluePin.GetComponent<Pin>();
        SoundManager._instance.PlaySoundEffect(m_playerChoosingSound);
    }

    public void ReleasePin()
    {
        m_currentPlayerPin.DecrementUsages();
    }

    public void ReloadPin(Pin pin)
    {
        pin.IncrementUsages();
    }

    private void LevelIsOver()
    {
        Debug.Log("Level is over");
        m_isLevelOver = true;
        m_currentPlayerPin = null;
    }

}
