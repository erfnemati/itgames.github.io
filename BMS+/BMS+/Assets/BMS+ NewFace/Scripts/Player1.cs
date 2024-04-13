using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 :IGameService
{
    public Pin1 m_currentPlayerPin {  get; private set; }

    private AudioClip m_playerChoosingSound;
    LevelManager m_levelManager;

    public Player1()
    {
        ServiceLocator._instance.Register(this);
        AddEvents();
        InitializeVariables();
    }

    private void AddEvents()
    {
    }
    private void InitializeVariables()
    {
        m_playerChoosingSound=ServiceLocator._instance.Get<DataManager>().GetData<ConfigData.SoundConfigData>((int)GameEnums.SoundName.playerChoosingSound).audioClip;
    }
    public void PreDestroy()
    {
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



}
