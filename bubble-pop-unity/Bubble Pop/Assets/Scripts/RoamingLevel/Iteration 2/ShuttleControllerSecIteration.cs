using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ShuttleControllerSecIteration : MonoBehaviour
{
    [SerializeField] Sprite m_startedShuttleSprite;
    [SerializeField] Transform m_targetTransform;
    [SerializeField] float m_translateSpeed;
    [SerializeField] float m_translationCycleTime;
    [SerializeField] float m_finalScale;

    [SerializeField] int m_numOfNeededFuelBubbles;
    private int m_numOfAchievedFuelBubbles;

    [SerializeField] ShuttleUiControllerSecIteration m_shuttleUiScript;

    

    public void StartShuttle()
    {
        m_shuttleUiScript.DeactivateUiComponent();
        GetComponent<SpriteRenderer>().sprite = m_startedShuttleSprite;
        transform.DOMove(m_targetTransform.position, m_translationCycleTime);
        transform.DOScale(m_finalScale, m_translationCycleTime).OnComplete(() => ShowResultMenu()).OnComplete(()=>Destroy(this.gameObject));
    }

    private void ShowResultMenu()
    {
        OverlayUiController._instance.ShowResultMenu();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FuelBubble"))
        {
            RoamingInputHandler._instance.SetIsDragging(false);
            Destroy(other.gameObject);
            AchieveFuelBubble();
            m_shuttleUiScript.UpdateSlider(m_numOfAchievedFuelBubbles);
        }
        else
        {
            Debug.Log("I am here");
        }
    }

    private void AchieveFuelBubble()
    {
        m_numOfAchievedFuelBubbles++;
        CheckIsShuttleReady();
    }

    private void CheckIsShuttleReady()
    {
        if  (m_numOfAchievedFuelBubbles >= m_numOfNeededFuelBubbles)
        {
            StartShuttle();
        }
    }
}
