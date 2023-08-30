using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AstronutHead : MonoBehaviour
{
    [SerializeField] float m_finalScale;
    [SerializeField] Canvas m_headCanvas;
    [SerializeField] Transform m_astronutHeadInShuttle;

    private void OnEnable()
    {
        PopHead();
    }

    private void PopHead()
    {
        transform.DOScale(m_finalScale, 1f);
    }

    public void SquizHead()
    {
        transform.DOMove(m_astronutHeadInShuttle.position, 1f);
        transform.DOScale(0f, 1f);
    }
}
