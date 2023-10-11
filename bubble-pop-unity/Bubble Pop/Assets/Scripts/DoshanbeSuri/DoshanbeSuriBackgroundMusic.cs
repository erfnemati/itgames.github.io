using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoshanbeSuriBackgroundMusic : MonoBehaviour
{
    [SerializeField]AudioSource m_backgroundAudioSource;
    public void FadeOutMusic()
    {
        m_backgroundAudioSource.DOFade(0.0f, 2f);
    }
}
