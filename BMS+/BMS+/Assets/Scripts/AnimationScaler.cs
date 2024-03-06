using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class AnimationScaler : MonoBehaviour
{
    
    [SerializeField] float m_finalScale;
    private Tween m_currentTween;

    private void Start()
    {
        DOTween.Init();
        m_currentTween = transform.DOScale(m_finalScale, 0.75f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        if (m_currentTween != null)
        {
            m_currentTween.Kill();
        }
        
    }
}
