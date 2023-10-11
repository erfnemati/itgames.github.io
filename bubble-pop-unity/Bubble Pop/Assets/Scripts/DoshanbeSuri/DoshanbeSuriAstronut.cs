using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoshanbeSuriAstronut : MonoBehaviour
{
    [SerializeField] Transform m_finalPos;
    [SerializeField] GameObject m_astronutCanvas;
    void Start()
    {
        MoveAstronut();
    }

    private async void  MoveAstronut()
    {
        await this.transform.DOMove(m_finalPos.position, 5f).AsyncWaitForCompletion();
        PopDialogueBox();
    }
    
    private void PopDialogueBox()
    {
        m_astronutCanvas.gameObject.SetActive(true);
        float m_initialScale = m_astronutCanvas.GetComponent<RectTransform>().localScale.x;
        m_astronutCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
        m_astronutCanvas.GetComponent<RectTransform>().DOScale(m_initialScale, 0.5f); 
    }
}
