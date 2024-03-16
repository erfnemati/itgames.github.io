using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public static Player1 _instance;
    public Pin1 m_currentPlayerPin {  get; private set; }
    private bool m_isLevelOver = false;

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

    public Pin1 GetPlayerPin()
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

    public void PickPin(Pin1 selectedPin)
    {
        if(m_isLevelOver)
        {
            return;
        }
        if (m_currentPlayerPin != null)
        {
            m_currentPlayerPin.ResetPinUi();
        }
        m_currentPlayerPin = selectedPin;
        SoundManager._instance.PlaySoundEffect(m_playerChoosingSound);
    }
    /// <summary>
    /// these should be combined
    /// </summary>

    public void ReleasePin()
    {
        m_currentPlayerPin.DecrementUsages();
    }

    public void ReloadPin(Pin1 pin)
    {
        pin.IncrementUsages();
    }

    private void LevelIsOver() //should not be here
    {
        Debug.Log("Level is over");
        m_isLevelOver = true;
        m_currentPlayerPin = null;
    }

}
