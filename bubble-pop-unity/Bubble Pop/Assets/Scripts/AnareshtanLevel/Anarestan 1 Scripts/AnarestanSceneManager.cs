using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnarestanSceneManager : MonoBehaviour
{
    [SerializeField] GameObject m_thinkingScene;
    [SerializeField] GameObject m_sleepingScene;
    GameObject m_currentScene;

    [SerializeField] GameObject m_sleepingVfx;
    [SerializeField] GameObject m_dreamVfx;

    [SerializeField] GameObject m_ItemGenerator;
    [SerializeField] GameObject m_player;

    


   
    void Start()
    {
        m_currentScene = m_sleepingScene;
        Invoke(nameof(ActivateThinkingScene),3f);
    }

    private void ActivateThinkingScene()
    {
        m_currentScene.gameObject.SetActive(false);
        m_thinkingScene.gameObject.SetActive(true);
        m_currentScene = m_thinkingScene;
    }

    private void ActivateSleepingScene()
    {
        m_currentScene.gameObject.SetActive(false);
        m_sleepingScene.gameObject.SetActive(true);
        m_currentScene = m_sleepingScene;

    }

    private void ActivateSleepingVfx()
    {
        m_sleepingVfx.gameObject.SetActive(true);
    }

    private void ActivateDreamVfx()
    {
        m_dreamVfx.gameObject.SetActive(true);
    }

    private void StartDream()
    {
        m_currentScene.gameObject.SetActive(false);
        m_player.gameObject.SetActive(true);
        m_ItemGenerator.gameObject.SetActive(true);
        m_dreamVfx.gameObject.SetActive(false);
        m_sleepingVfx.gameObject.SetActive(false);
    }

    public void StartSleeping()
    {
        ActivateSleepingScene();
        ActivateSleepingVfx();
        Invoke(nameof(ActivateDreamVfx), 2f);
        Invoke(nameof(StartDream), 6f);

    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
