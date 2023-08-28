using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThirdRoamingGameManager : MonoBehaviour
{
    public static ThirdRoamingGameManager _instance;
    [SerializeField] ShuttlePartGenerator m_shuttlePartGenerator;
    [SerializeField] RoamingAstronut m_roamingAstronut;
    [SerializeField] ShuttleControllerThirdEdition m_shuttle; 
    [SerializeField] int m_numOfNeededParts;
    [SerializeField] int m_numOfAchievedShuttleParts = 0;


    [SerializeField] GameObject m_roamingBubble;
    [SerializeField] Transform m_roamingBubbleTransform;
    

    


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        GoNextState();
    }
    public void CheckIsLevelEnded()
    {
        if (m_numOfAchievedShuttleParts >= m_numOfNeededParts)
        {
            ComeAstronut();
        }
    }

    public void GoNextState()
    {
        m_shuttlePartGenerator.GenerateShuttlePart();
        m_numOfAchievedShuttleParts++;
        CheckIsLevelEnded();
    }

    public void RestartState()
    {
        m_shuttlePartGenerator.ReGenerateShuttlePart();
    }

    private void ComeAstronut()
    {
        m_roamingAstronut.GoToShuttle();
    }

    public  void StartShuttleFirstTime()
    {
        m_shuttle.StartWithoutRoaming();
    }

    public void StartShuttleFinalTime()
    {
        m_shuttle.StartWithRoaming();
    }

    public void GenerateRoamingBubble()
    {
        GameObject temp = Instantiate(m_roamingBubble, m_roamingBubbleTransform.position, Quaternion.identity);
        
    }

    public void ShowResultMenu()
    {
        OverlayUiController._instance.ShowResultMenu();
    }



    
}
