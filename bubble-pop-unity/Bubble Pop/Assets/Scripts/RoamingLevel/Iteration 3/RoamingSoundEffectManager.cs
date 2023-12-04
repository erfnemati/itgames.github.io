using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingSoundEffectManager : MonoBehaviour
{
    public static RoamingSoundEffectManager _instance;
    [SerializeField] AudioSource m_audioSource;
    void Start()
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

    public void PlaySound(AudioClip soundEffect)
    {
        m_audioSource.clip = soundEffect;
        m_audioSource.Play();
    }
}
