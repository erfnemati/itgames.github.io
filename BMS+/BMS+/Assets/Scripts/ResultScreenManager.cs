using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;
using UnityEngine.SceneManagement;


public class ResultScreenManager : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro m_timeRecord, m_HpCounter, m_playerRank,m_lastPassedLvl;
    PlayerPersistentData m_currentPlayerData;

    private void Start()
    {
        m_currentPlayerData = ServiceLocator._instance.Get<PersistentDataManager>().GetCurrentPlayerData();
        SetTimeRecord();
        SetHpCounter();
        SetPlayerRank();
        SetPlayerLastPassedLvl();
    }

    private void SetPlayerLastPassedLvl()
    {
        m_lastPassedLvl.text ="آخرین مرحله : " + m_currentPlayerData.GetPlayerLastLevel().ToString();
    }

    private void SetTimeRecord()
    {
        float time = m_currentPlayerData.GetPlayingTime();
        m_timeRecord.text =  "زمان : " + ((int)time).ToString() +  " ثانیه ";
    }

    private void SetHpCounter()
    {
        int hpCounter = m_currentPlayerData.GetNumOfConsumedLives();
        m_HpCounter.text = "آنتن از دست رفته : " + hpCounter.ToString();
    }

    private void SetPlayerRank()
    {
        float playerCluster = ServiceLocator._instance.Get<PersistentDataManager>().GetPlayerCluster();

        if (playerCluster - 0.25f < Mathf.Epsilon)
        {
            m_playerRank.text = "تو جزو 25 درصد برتر بازکنان هستی!";
        }
        else if (playerCluster - 0.5f < Mathf.Epsilon && playerCluster - 0.25f > Mathf.Epsilon)
        {
            m_playerRank.text = "عملکردت در بازی شبیه میانگین بازیکنان هست!";
        }
        else if(playerCluster -0.75f <Mathf.Epsilon && playerCluster - 0.5f > Mathf.Epsilon)
        {
            m_playerRank.text = "کمی بیش‌تر سعی کن از میانگین بازیکنا عقبی!";
        }
        
        else
        {
            m_playerRank.text = "باید خیلی بیش‌تر تلاش کنی،اکثر بازیکنا ازت جلوترن";
        }   
    }

    public void QuitFromResultScreen()
    {
        ServiceLocator._instance.Get<BmsPlusSceneManager>().LoadMainMenu();
    }
}
