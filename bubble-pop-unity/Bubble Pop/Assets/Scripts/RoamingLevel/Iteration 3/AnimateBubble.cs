using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimateBubble : MonoBehaviour
{
    //Parameters for bubble moving : 
    [SerializeField] float m_movingDistance;
    [SerializeField] float m_cycleTime;
    private bool m_isDragged = false;
    Tween m_movingTween;

    private void Start()
    {
        MoveBubble();
    }
    

    private void MoveBubble()
    {
        this.transform.DOMove(transform.position + new Vector3(0, m_movingDistance, 0), m_cycleTime).SetLoops(-1,LoopType.Yoyo);
    }

    public void StopBubble()
    {
        DOTween.Kill(this.transform); 
    }

    
}
