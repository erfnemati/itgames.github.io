using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManager : MonoBehaviour, IGameService
{
    [SerializeField] AudioSource m_musicSource;
    [SerializeField] AudioSource m_soundEffectSource;
    [SerializeField] bool m_isMute = false;

    [SerializeField]private float m_lastMusicVolume;
    [SerializeField]private float m_lastSoundEffectVolume;

    private void Start()
    {
        m_lastMusicVolume = m_musicSource.volume;
        m_lastSoundEffectVolume = m_soundEffectSource.volume;
        m_musicSource = GetComponentsInChildren<AudioSource>()[0];
        m_soundEffectSource = GetComponentsInChildren<AudioSource>()[1];
    }


    public void PlaySoundEffect(AudioClip clip)
    {
        m_soundEffectSource.PlayOneShot(clip);
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        m_musicSource.clip = clip;
        m_musicSource.Play();
    }

    public void StopAllSoundEffects()
    {
        if (m_soundEffectSource.isPlaying)
        {
            m_soundEffectSource.Stop();
        }
        else
        {
            
        }
    }

    public void MuteSound()
    {
        m_lastMusicVolume = m_musicSource.volume;
        m_lastSoundEffectVolume = m_soundEffectSource.volume;
        m_musicSource.volume = 0.0f;
        m_soundEffectSource.volume = 0.0f;
        m_isMute = true;
    }

    public void UnmuteSound()
    {
        m_musicSource.volume = m_lastMusicVolume;
        m_soundEffectSource.volume = m_lastSoundEffectVolume;
        m_isMute = false;
    }

    public bool GetMuteState()
    {
        return m_isMute;
    }
    public void OnDestroy()
    {
    }


}
