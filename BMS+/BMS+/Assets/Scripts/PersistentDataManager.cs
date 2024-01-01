using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PersistentDataManager : MonoBehaviour
{

    public const string m_playersInfoString = "PlayersInfo";
    public static PersistentDataManager _instance;

    PlayersInfo m_playersInfo;
    PlayerPersistentData m_currentData;

    private bool m_isLevelOver = false;

    [SerializeField] string m_phoneNumer;
    [SerializeField] int m_numOfConsumedLives;
    [SerializeField] float m_passedTime;
    [SerializeField] int m_playerLastLevel;

    private void OnEnable()
    {
        LevelManager.OnLevelDefeat += IncrementNumOfConsumedLives;
        PhoneScreenManager.EndLevel += TurnOffTimer;
        PlayerLifeManager.GameIsOver += GetLevel;
        LevelManager.OnLevelRetreat += GetLevel;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelDefeat -= IncrementNumOfConsumedLives;
        PhoneScreenManager.EndLevel -= TurnOffTimer;
        PlayerLifeManager.GameIsOver -= GetLevel;
        LevelManager.OnLevelRetreat -= GetLevel;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        m_playersInfo = new PlayersInfo();
        Debug.Log("Instantiating new playersInfo object");
        StartNewPlaythrough();
        RetrieveData();
    }

    private void Update()
    {
        if (m_currentData != null && m_isLevelOver == false )
        {
            m_currentData.UpdateTime(Time.deltaTime);
            m_passedTime = m_currentData.GetPlayingTime();
        }
    }

    public void StartNewPlaythrough()
    {
        m_currentData = new PlayerPersistentData();
    }

    public void AddData(string phoneNumber)
    {
        m_playersInfo.m_playersInfoList.Add
            (new PlayerPersistentData
                (phoneNumber,m_currentData.GetNumOfConsumedLives(),m_currentData.GetPlayingTime(),m_currentData.GetPlayerLastLevel())
            );
    }

    public void SaveData(string enteredText)
    {
        string phoneNumber = enteredText;

        SetPhoneNumber(phoneNumber);
        AddData(phoneNumber);

        string jsonString = JsonUtility.ToJson(m_playersInfo,true);
        PlayerPrefs.SetString(m_playersInfoString, jsonString);
        PlayerPrefs.Save();
    }

    private void SetPhoneNumber(string phoneNumber)
    {
        m_currentData.SetPhoneNumber(phoneNumber);
    }

    public void RetrieveData()
    {
        Debug.Log("Retriving");
        string jsonString = PlayerPrefs.GetString(m_playersInfoString, "Empty");
        Debug.Log(jsonString);

        m_playersInfo = JsonUtility.FromJson<PlayersInfo>(jsonString);
    }

    public void IncrementNumOfConsumedLives()
    {
        Debug.Log("Increasing number of lives");
        m_currentData.IncrementConsumedLives();
    }

    public void TurnOffTimer()
    {
        Debug.Log("Timer is off");
        m_isLevelOver = true;
    }

    public PlayerPersistentData GetCurrentPlayerData()
    {
        return m_currentData;
    }

    public void GetLevel()
    {
        Debug.Log("Getting Level");
        int level = BmsPlusSceneManager._instance.GetCurrentLevel();
        m_currentData.SetPlayerLastLevel(level -1);
        m_playerLastLevel = level -1;
    }
}

[Serializable]
public class PlayersInfo
{
    public List<PlayerPersistentData> m_playersInfoList = new List<PlayerPersistentData>();

    public  void SortListBasedOnTime()
    {
        //To be implemented.
    }
}
