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


    //These parameters are for tutorial :
    private bool m_isPlayingForFirstTime = true;
    [SerializeField] GameObject m_tutorialhand;
    [SerializeField] GameObject m_basisShuttlePart;
    [SerializeField] GameObject m_DialogueBox;


    //These parameters are for need one more thing part : 
    [SerializeField] GameObject m_blurredBackground;
    [SerializeField] GameObject m_astronutHeadObject;
    [SerializeField] float m_finalScale;
    [SerializeField] Transform m_finalTransfrom;

    [SerializeField] GameObject m_winningVfx;
    [SerializeField] AudioSource m_backgroundMusic;

    


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
        if (m_isPlayingForFirstTime)
        {
            PlayTutorial();
        }
        else
        {
            GoNextState();
        }
        
    }


    private async void PlayTutorial()
    {
        await m_roamingAstronut.ArriveForFirstTime();
    }

    public void GenerateFirstShuttlePart()
    {
        m_basisShuttlePart.SetActive(true);
        m_basisShuttlePart.transform.DOScale(0.2f, 0.5f);
        ShowDragTutorial();
    }

    private void ShowDragTutorial()
    {
        m_DialogueBox.gameObject.SetActive(true);
        m_DialogueBox.GetComponent<RectTransform>().DOScale(1, 1f);
        m_tutorialhand.gameObject.SetActive(true);
        float finalYPos = m_tutorialhand.transform.position.y - 4f;
        m_tutorialhand.transform.DOMoveY(finalYPos, 1.5f).SetLoops(-1, LoopType.Restart);
    }

    public void EndTutorial()
    {
        m_tutorialhand.gameObject.SetActive(false);
        m_DialogueBox.gameObject.SetActive(false);
        m_roamingAstronut.GetToInitialPlace();
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

    private async void ComeAstronut()
    {
        await m_roamingAstronut.GoToShuttle();
        m_shuttle.TurnOnShuttle();
        Invoke(nameof(StartShuttleFirstTime),1f);
    }

    public  void StartShuttleFirstTime()
    {
        Destroy(m_roamingAstronut.gameObject);
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

    public void ShowUnhappyAstronut()
    {
        var sequence = DOTween.Sequence();
        m_blurredBackground.SetActive(true);
        m_astronutHeadObject.SetActive(true);
        sequence.Append(m_astronutHeadObject.GetComponent<RectTransform>().DOScale(m_finalScale, 1f));
        sequence.AppendInterval(2f).OnComplete(()=>HideUnhappyAstronutFace());
    }

    public async void HideUnhappyAstronutFace()
    {
        m_astronutHeadObject.GetComponent<RectTransform>().DOScale(0, 1f);
        await m_astronutHeadObject.GetComponent<RectTransform>().DOMove(m_finalTransfrom.position, 1f).AsyncWaitForCompletion();
        m_blurredBackground.gameObject.SetActive(false);
        m_astronutHeadObject.SetActive(false);
        GenerateRoamingBubble();
    }

    public void ActivateWiningVfx()
    {
        m_winningVfx.gameObject.SetActive(true);
        Invoke(nameof(ShowResultMenu), 1.5f);
    }
    public void ShowResultMenu()
    {
        OverlayUiController._instance.ShowResultMenu();
        m_backgroundMusic.DOFade(0f, 1f);
    }

    



    
}
