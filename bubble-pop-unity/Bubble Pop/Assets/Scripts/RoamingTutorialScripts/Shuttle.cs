using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shuttle : MonoBehaviour
{
    [SerializeField] Transform m_marsTransform;
    [SerializeField] float m_transformCycle;
    [SerializeField] Sprite m_astronutInShuttle;
    [SerializeField] float m_finalScale;
    [SerializeField] float m_scaleCycle;
    [SerializeField] GameObject m_Menu;
    
    public void ChangeSprite()
    {
        GetComponent<SpriteRenderer>().sprite = m_astronutInShuttle;
        TranvelToMarsPartOne();
    }

    public void TranvelToMarsPartOne()
    {
        this.transform.DOMove(m_marsTransform.position, m_transformCycle);
        this.transform.DOScale(m_finalScale, m_scaleCycle).OnComplete(()=>PopMainMenu());
    }

    private void PopMainMenu()
    {
        m_Menu.SetActive(true);
    }

    
}
