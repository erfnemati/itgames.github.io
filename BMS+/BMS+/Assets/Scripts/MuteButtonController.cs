using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MuteButtonController : MonoBehaviour
{
    private Image m_buttonImage;
    [SerializeField] Sprite m_muteSprite;
    [SerializeField] Sprite m_unMuteSprite;
    private bool m_isMute = false;

    private void Start()
    {
        m_buttonImage = GetComponentInChildren<Image>();
        UpdateButtonImage();
        m_isMute = SoundManager._instance.GetMuteState();
    }

    private void UpdateButtonImage()
    {
        bool muteState = SoundManager._instance.GetMuteState();
        
        if (muteState == true)
        {
            m_buttonImage.sprite = m_muteSprite;
        }
        else
        {
            m_buttonImage.sprite = m_unMuteSprite;
        }
    }

    public void SwitchMuteButton()
    {
        if (m_isMute == false)
        {
            SoundManager._instance.MuteSound();
            m_isMute = true;
            
        }
        else
        {
            SoundManager._instance.UnmuteSound();
            m_isMute = false;
        }
        UpdateButtonImage();
    }


}
