using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 :IGameService
{
    public static Player1 _instance;
    public Pin1 m_currentPlayerPin {  get; private set; }
    private bool m_isLevelOver = false;

    private AudioClip m_playerChoosingSound;
    

    public Player1()
    {
        ServiceLocator.Current.Register(this);
        AddEvents();
        InitializeVariables();
    }

    private void AddEvents()
    {
        LevelManager.OnLevelDefeat += LevelIsOver;
        LevelManager.OnLevelRetreat += LevelIsOver;
        LevelManager.OnLevelVictory += LevelIsOver;
    }
    private void InitializeVariables()
    {
        m_playerChoosingSound=DataManager._instance.GetData<SoundData>((int)GameEnums.SoundName.playerChoosingSound).audioClip;
    }
    public void PreDestroy()
    {
        LevelManager.OnLevelDefeat -= LevelIsOver;
        LevelManager.OnLevelRetreat -= LevelIsOver;
        LevelManager.OnLevelVictory -= LevelIsOver;
        //Debug.Log("Player Disabling");
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
