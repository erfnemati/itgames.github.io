using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Tutotrial
{
    public class TipBoxManager : MonoBehaviour
    {
        private Tween m_currentTween;
        private Vector3 m_initialScale;

        private void Start()
        {
            m_initialScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.localScale = Vector3.zero;
            m_currentTween = transform.DOScale(m_initialScale, 0.5f).SetEase(Ease.InCubic);


        }

        private void OnDisable()
        {
            if (m_currentTween != null)
            {
                m_currentTween.Kill();
            }
        }
    }
}
