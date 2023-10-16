using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.UI;
using Assets.Scripts;

public class AlwaysOnNigthsGameManager : MonoBehaviour
{
    public static AlwaysOnNigthsGameManager _instance;

    [SerializeField] Image timebar;

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
    [SerializeField] GameObject showdialogbeforemoonpopup;
    [SerializeField] GameObject overlay;
    [SerializeField] GameObject endchatpop;
    [SerializeField] GameObject poperhead;

    [SerializeField] GameObject vfx;

   
    //Parameters for tutorial : 
    private bool m_isTutorialOver = false;
    [SerializeField] GameObject m_tutorialBox;

    public float delay = 3.0f;
    public float maxtimeav;
    [SerializeField] AudioSource win;
    [SerializeField] AudioSource startcall;
    [SerializeField] AudioSource soundthem;
    // [SerializeField] AudioSource soundtrack;

    public bool setfalse;

    private void Start()
    {
        maxtimeav = m_remainingTime;
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
    //timer ch00b1n

    public void Foo()
    {
        StartCoroutine(TemporarilyDeactivate(0));
    }

    private IEnumerator TemporarilyDeactivate(float duration)
    {
      
        //Time.timeScale = 1.0f;
        yield return new WaitForSeconds(duration);
        //overlay.SetActive(false);
       // showdialogbeforemoonpopup.SetActive(false);

        Debug.Log("endtimer");
     //   Time.timeScale = 1.0f;

       // showdialogbeforemoonpopup.SetActive(false);
      //  Time.timeScale = 1.0f;
    }
    //timer ch00b1n

    private void UpdateRemainingTime()
    {
       // soundtrack.Play();
       // startcall.Stop();
        m_remainingTime -= Time.deltaTime;
        timebar.fillAmount = 1 - (m_remainingTime / maxtimeav);
        
        if (m_remainingTime <= Mathf.Epsilon)
        {
            //-------ch00b1n-------
            //float delaytimer = 5.0f;

            //ui show dialoug poping moon
            //showdialogbeforemoonpopup.SetActive(true);
            // Foo();
         //   overlay.SetActive(true);
            showdialogbeforemoonpopup.SetActive(true);
            timebar.enabled = false;
            Time.timeScale = 0.0f;

            // 
            //if ()
            {
                //Time.timeScale = 1.0f;
                //overlay.SetActive(false);
                //showdialogbeforemoonpopup.SetActive(false);
            }
            //else
            //{
               
            //}


            //Foo();
            //else
            //{
            //    overlay.SetActive(false);
            //    showdialogbeforemoonpopup.SetActive(false);
            //    Time.timeScale = 1.0f;

            //}
            //timer
            // delaytimer -= Time.deltaTime;
            // showdialogbeforemoonpopup.SetActive(false);
            //-------ch00b1n-------
            //Debug.Log("stop");
            GenerateMoonBubble();

        }
    }

    public void resumgame()
    {
        Time.timeScale = 1.0f;
        overlay.SetActive(false);
        showdialogbeforemoonpopup.SetActive(false);

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
        startcall.Play();
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
        soundthem.Stop();
        m_inputHandler.FinishGame();
        m_sceneManager.ShowLosingScreen();
        FindObjectOfType<AstronutValueController>().EndLevel();
        Time.timeScale = 0.0f;
    }

    //public void WinGame()
    //{
    //    endchatpop.SetActive(true);
    //  //  Time.timeScale = 0.0f;

    //    m_inputHandler.FinishGame();
    //    FindObjectOfType<AstronutValueController>().EndLevel();
    //    //m_spotLight.pointLightOuterRadius = 15;

    //    //----ch00b1n----
    //    soundthem.Stop();
    //    vfx.SetActive(true);
    //    win.Play();
    //    //----ch00b1n----
    //    DOTween.Init();
    //    DOTween.To(() => { return m_spotLight.pointLightOuterRadius; }, x => m_spotLight.pointLightOuterRadius = x, 14, 1)
    //        .OnComplete(() => EndGame());

    //}

    public void ActivateMoonBubble()
    {
        FindObjectOfType<AstronutValueController>().EndLevel();
        m_inputHandler.FinishGame();
        DOTween.Init();
        DOTween.To(() => { return m_spotLight.pointLightOuterRadius; }, x => m_spotLight.pointLightOuterRadius = x, 14, 1);
    }

    public void ActivateLastWords()
    {
        endchatpop.SetActive(true);
        Invoke(nameof(AcitvateWinningScreen), 1.5f);
    }

   

    public void AcitvateWinningScreen()
    {
        
        
        //m_spotLight.pointLightOuterRadius = 15;

        //----ch00b1n----
        soundthem.Stop();
        vfx.SetActive(true);
        win.Play();
        //----ch00b1n----

        Invoke(nameof(EndGame), 2f);
        
    }
   
    public void resforendchatpop()
    {
      //  Time.timeScale = 1.0f;
       // endchatpop.SetActive(false);
    }
    private void EndGame()
    {
        //; ;bn
        endchatpop.SetActive(false);
        Time.timeScale = 0.0f;
        m_sceneManager.ShowWinningScreen();
        

    }

    public void PauseGame()
    {
        soundthem.Stop();
        m_inputHandler.SetGamePauseState(true);
    }

    public void ResumeGame()
    {
        soundthem.Play();
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
        soundthem.Play();

    }

    //public void SetIsMoonGenerated(bool isMoonGenerated)
    //{
    //    m_isMoonGenerated = isMoonGenerated;
    //}
}
