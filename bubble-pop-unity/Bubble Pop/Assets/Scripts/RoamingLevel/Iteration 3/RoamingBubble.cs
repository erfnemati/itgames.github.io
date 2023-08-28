using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoamingBubble : MonoBehaviour
{
    Tween m_currentTween;
    void Start()
    {
        transform.DOScale(0.36f, 0.5f);
        Tween m_currentTween = transform.DOMove(transform.position + new Vector3(0, 0.5f, 0), 2f).SetLoops(-1, LoopType.Yoyo);
    }

    public void KillCurrentTween()
    {
        DOTween.Kill(this.transform);
    }

    public void ReviveTween()
    {
        Debug.Log("Hello");
        Tween m_currentTween = transform.DOMove(transform.position + new Vector3(0, 0.5f, 0), 2f).SetLoops(-1, LoopType.Yoyo);
    }

    
}
