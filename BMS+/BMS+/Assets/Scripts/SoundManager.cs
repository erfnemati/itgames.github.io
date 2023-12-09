using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    [SerializeField] AudioSource m_musicSource;
    [SerializeField] AudioSource m_soundEffectSource;


    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
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
            Debug.Log("Stopping sound");
            m_soundEffectSource.Stop();
        }
        else
        {
            Debug.Log("noting is playing");
        }
    }
}
