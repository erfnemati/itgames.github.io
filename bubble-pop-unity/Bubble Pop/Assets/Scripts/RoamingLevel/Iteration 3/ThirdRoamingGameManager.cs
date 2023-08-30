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

    [SerializeField] List<GameObject> m_enemyStates = new List<GameObject>();
    private int m_enemyStateIndex = 0;
    private GameObject m_lastEnemyState = null;

    private bool m_isLevelEnded = false;

    


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
            Debug.Log("Level is ended");
            m_isLevelEnded = true;
            DeactivateLastState();
            ComeAstronut();
        }
    }

    public void GoNextState()
    {
        CheckIsLevelEnded();
        if (m_isLevelEnded)
        {
            return;
        }
        m_shuttlePartGenerator.GenerateShuttlePart();
        DeactivateLastState();
        m_enemyStates[m_enemyStateIndex].SetActive(true);
        m_lastEnemyState = m_enemyStates[m_enemyStateIndex];
        m_enemyStateIndex = (m_enemyStateIndex + 1) % m_enemyStates.Count;
        m_numOfAchievedShuttleParts++;
        

    }

    private void DeactivateLastState()
    {
        if(m_lastEnemyState != null)
        {
            m_lastEnemyState.SetActive(false);
        }
    }

    public void RestartState()
    {
        if (m_isLevelEnded)
        {
            return;
        }
        Debug.Log("Restarting");
        DeactivateLastState();
        m_shuttlePartGenerator.ReGenerateShuttlePart();
        m_lastEnemyState.SetActive(true);
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
