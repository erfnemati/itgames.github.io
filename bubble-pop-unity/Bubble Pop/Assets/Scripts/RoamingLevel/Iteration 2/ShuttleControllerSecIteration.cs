using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ShuttleControllerSecIteration : MonoBehaviour
{
    
    
    [SerializeField] Transform m_targetTransform;
    [SerializeField] float m_translateSpeed;
    [SerializeField] float m_translationCycleTime;
    [SerializeField] float m_finalScale;
    

    [SerializeField] int m_numOfNeededFuelBubbles;
    private int m_numOfAchievedFuelBubbles;

    [SerializeField] ShuttleUiControllerSecIteration m_shuttleUiScript;
    [SerializeField] RoamingGameManager m_gameManagerScript;

    private const int NUMBER_OF_WAYPOINTS = 5;
    [SerializeField] Transform[] m_waypointTransforms = new Transform[NUMBER_OF_WAYPOINTS];
    private Vector3[] m_waypoints = new Vector3[NUMBER_OF_WAYPOINTS];

    private void Start()
    {
        m_shuttleUiScript.SetSliderMaxValue(m_numOfNeededFuelBubbles);
        GetPosFromTransform();
    }

    private void GetPosFromTransform()
    {
        for(int i = 0; i < NUMBER_OF_WAYPOINTS; i++)
        {
            m_waypoints[i] = m_waypointTransforms[i].position;
        }
    }

    public void StartShuttle()
    {
        Debug.Log("Going Up");
        m_shuttleUiScript.DeactivateUiComponent();
        m_shuttleUiScript.GoToShuttle();

        Invoke(nameof(MoveShuttle), 1f);
    }

    private void MoveShuttle()
    {
        transform.DOPath(m_waypoints, m_translationCycleTime, PathType.CatmullRom, PathMode.TopDown2D, 5, Color.red);
        transform.DORotate(new Vector3(0, 0, 50), m_translationCycleTime * 2);
        transform.DOScale(m_finalScale, m_translationCycleTime).OnComplete(() => ShowResultMenu());
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
            m_gameManagerScript.LoadNextState();
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
            m_gameManagerScript.EndLevel();
        }
    }
}
