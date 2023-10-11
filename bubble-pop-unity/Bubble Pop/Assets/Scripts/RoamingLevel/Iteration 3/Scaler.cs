using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scaler : MonoBehaviour
{
    [SerializeField] float m_finalScale;

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(m_finalScale, 0.5f);
    }

}
