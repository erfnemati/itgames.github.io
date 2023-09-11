using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class AlwaysOnNigthsGameManager : MonoBehaviour
{
    public static AlwaysOnNigthsGameManager _instance;

    //Parameters for running game logic : 
    [SerializeField] AlwaysOnInputHandler m_inputHandler;
    [SerializeField] float m_passedTime = 0f;
    [SerializeField] float m_secNeedToWin = 30f;
    [SerializeField] Light2D m_spotLight;

    //Parameters for generating moon : 
    [SerializeField] List<Transform> m_moonSpots = new List<Transform>();
    [SerializeField] GameObject m_moonObject;
    private bool m_isMoonGenerated = false;

    //Parameters for Ui part of game manager : 
    [SerializeField] AlwaysOnNightGameManagerUi m_uiManager;

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

        m_uiManager.InitialiseSlider(m_secNeedToWin);
    }

    private void Update()
    {
        if (m_isMoonGenerated)
            return;

        UpdatePassedTime();
        m_uiManager.UpdateSlider(m_passedTime);

    }

    private void UpdatePassedTime()
    {
        m_passedTime += Time.deltaTime;
        

        if (m_passedTime - m_secNeedToWin > Mathf.Epsilon)
        {
            GenerateMoonBubble();
        }
    }

    public void CheckValue(float addedValue)
    {
        Debug.Log(addedValue);
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

    private void LoseGame()
    {
        Debug.Log("You have lost");
        m_inputHandler.FinishGame();
        Time.timeScale = 0.0f;
        
    }

    public void WinGame()
    {
        m_inputHandler.FinishGame();
        FindObjectOfType<AstronutValueController>().EndLevel();
        //m_spotLight.pointLightOuterRadius = 15;

        DOTween.Init();
        DOTween.To(() => { return m_spotLight.pointLightOuterRadius; }, x => m_spotLight.pointLightOuterRadius = x, 15, 2)
            .OnComplete(()=>EndGame());
    }

    private void EndGame()
    {
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

    //public void SetIsMoonGenerated(bool isMoonGenerated)
    //{
    //    m_isMoonGenerated = isMoonGenerated;
    //}
}
