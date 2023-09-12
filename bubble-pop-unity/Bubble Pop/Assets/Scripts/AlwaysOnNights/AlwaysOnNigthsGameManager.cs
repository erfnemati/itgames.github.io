using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using Assets.Scripts;

public class AlwaysOnNigthsGameManager : MonoBehaviour
{
    public static AlwaysOnNigthsGameManager _instance;

    //Parameters for running game logic : 
    [SerializeField] AlwaysOnInputHandler m_inputHandler;
    [SerializeField] AstronutAutoMovement m_autoMovement;
    [SerializeField] AstronutValueController m_valueController;
    [SerializeField] GameObject m_valueBubbleGenerator;
    [SerializeField] GameObject m_tutorialStar;
    [SerializeField] Transform m_tutorialStartTransform;
    [SerializeField] GameObject m_tutorialHandPrefab;
    [SerializeField] GameObject m_tutorialHand;
    [SerializeField] Transform m_tutorialHandTransform;
    [SerializeField] float m_remainingTime = 30f;
    [SerializeField] float m_secNeedToWin = 30f;
    [SerializeField] Light2D m_spotLight;

    //Parameters for generating moon : 
    [SerializeField] List<Transform> m_moonSpots = new List<Transform>();
    [SerializeField] GameObject m_moonObject;
    private bool m_isMoonGenerated = false;

    //Parameters for Ui part of game manager : 
    [SerializeField] AlwaysOnNightGameManagerUi m_uiManager;
    [SerializeField] AlwaysOnNightsSceneManager m_sceneManager;

    //Parameters for tutorial : 
    private bool m_isTutorialOver = false;
    [SerializeField] GameObject m_tutorialBox;

    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        
        m_uiManager.InitialiseSlider(m_remainingTime);
        PlayTutorial();
    }

    private void Update()
    {
        if (m_isTutorialOver == false)
            return;
        if (m_isMoonGenerated)
            return;

        UpdateRemainingTime();
        m_uiManager.UpdateSlider(m_remainingTime);

    }

    private void UpdateRemainingTime()
    {
        m_remainingTime -= Time.deltaTime;
        

        if (m_remainingTime <= Mathf.Epsilon)
        {
            GenerateMoonBubble();
        }
    }

    public void CheckValue(float addedValue)
    {
        
        if (addedValue < Mathf.Epsilon)
        {
            Debug.Log("Losing here");
            LoseGame();
        }
    }

    private void GenerateMoonBubble()
    {
        int randomIndex = Random.Range(0, m_moonSpots.Count);
        GameObject tempObject = Instantiate(m_moonObject, m_moonSpots[randomIndex].position, Quaternion.identity);
        m_isMoonGenerated = true;
        RemoveStars();
    }

    private void RemoveStars()
    {
        AddedValueBubble[] stars = FindObjectsOfType<AddedValueBubble>();
        foreach(AddedValueBubble bubble in stars)
        {
            Destroy(bubble.gameObject);
        }
    }

    private async void PlayTutorial()
    {
        await m_autoMovement.ComeAstronut();
        
    }

    public void PopGuidelines()
    {
        PopTutorialDialogues();
        InstantiateTutorialStar();
        InstantiateTutorialHand();
    }

    private void InstantiateTutorialStar()
    {
        GameObject temp = Instantiate(m_tutorialStar, m_tutorialStartTransform.position, Quaternion.identity);
        temp.transform.localScale = Vector3.zero;
        temp.transform.DOScale(0.2f, 1f);
    }

    private void InstantiateTutorialHand()
    {
        m_tutorialHand = Instantiate(m_tutorialHandPrefab, m_tutorialHandTransform.position, Quaternion.identity);
        m_tutorialHand.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
        m_tutorialHand.transform.DOScale(0.4f, 1f).SetLoops(-1, LoopType.Yoyo); ;
    }

    private void PopTutorialDialogues()
    {
        m_tutorialBox.SetActive(true);
        m_tutorialBox.transform.localScale = Vector3.zero;
        m_tutorialBox.transform.DOScale(1, 1f);
    }

    

    private void LoseGame()
    {
        m_inputHandler.FinishGame();
        m_sceneManager.ShowLosingScreen();
        FindObjectOfType<AstronutValueController>().EndLevel();
        Time.timeScale = 0.0f;
    }

    public void WinGame()
    {
        m_inputHandler.FinishGame();
        FindObjectOfType<AstronutValueController>().EndLevel();
        //m_spotLight.pointLightOuterRadius = 15;

        DOTween.Init();
        DOTween.To(() => { return m_spotLight.pointLightOuterRadius; }, x => m_spotLight.pointLightOuterRadius = x, 14, 1)
            .OnComplete(()=>EndGame());
    }

    private void EndGame()
    {
        m_sceneManager.ShowWinningScreen();
        Time.timeScale = 0.0f;
    }

    public void PauseGame()
    {
        m_inputHandler.SetGamePauseState(true);
    }

    public void ResumeGame()
    {
        m_inputHandler.SetGamePauseState(false);
    }

    public void EndTutorial()
    {
        m_autoMovement.EndTutorial();
        m_valueController.EndTutorial();
        m_isTutorialOver = true;
        m_valueBubbleGenerator.SetActive(true);

        m_tutorialBox.gameObject.SetActive(false);
        m_tutorialHand.gameObject.SetActive(false);
    }

    //public void SetIsMoonGenerated(bool isMoonGenerated)
    //{
    //    m_isMoonGenerated = isMoonGenerated;
    //}
}
