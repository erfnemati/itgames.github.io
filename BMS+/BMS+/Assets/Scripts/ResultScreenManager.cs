using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;

public class ResultScreenManager : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro m_timeRecord, m_HpCounter, PlayerRank;
    PlayerPersistentData m_currentPlayerData;

    private void Start()
    {
        m_currentPlayerData = PersistentDataManager._instance.GetCurrentPlayerData();
        SetTimeRecord();
        SetHpCounter();
        SetPlayerRank();
    }

    private void SetTimeRecord()
    {
        float time = m_currentPlayerData.GetPlayingTime();
        m_timeRecord.text = m_timeRecord.text + ((int)time).ToString();
    }

    private void SetHpCounter()
    {
        int hpCounter = m_currentPlayerData.GetNumOfConsumedLives();
        m_HpCounter.text += hpCounter.ToString();
    }

    private void SetPlayerRank()
    {

    }
}
