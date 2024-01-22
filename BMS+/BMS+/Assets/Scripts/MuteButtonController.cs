using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MuteButtonController : MonoBehaviour
{
    private TextMeshProUGUI m_buttonText;
    private bool m_isMute = false;

    private void Start()
    {
        m_buttonText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateMuteButtonText();
        m_isMute = SoundManager._instance.GetMuteState();
    }

    private void UpdateMuteButtonText()
    {
        bool muteState = SoundManager._instance.GetMuteState();
        
        if (muteState == true)
        {
            m_buttonText.text = "UnMute";
        }
        else
        {
            m_buttonText.text = "Mute";
        }
    }

    public void SwitchMuteButton()
    {
        if (m_isMute == false)
        {
            SoundManager._instance.MuteSound();
            m_buttonText.text = "UnMute";
            m_isMute = true;
            
        }
        else
        {
            SoundManager._instance.UnmuteSound();
            m_buttonText.text = "Mute";
            m_isMute = false;
        }
    }


}
