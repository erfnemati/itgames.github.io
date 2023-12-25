using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PersistentDataManager : MonoBehaviour
{

    public static PersistentDataManager _instance;

    private TextInputManager m_textInputManager;
    public const string m_playersInfoString = "PlayersInfo";
    PlayersInfo m_playersInfo;
    PlayerPersistentData m_currentData;

    private void OnEnable()
    {
        LevelManager.OnLevelDefeat += IncrementNumOfConsumedLives;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelDefeat -= IncrementNumOfConsumedLives;
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
        m_textInputManager = GetComponent<TextInputManager>();
        m_playersInfo = new PlayersInfo();
        Debug.Log("Instantiating new playersInfo object");
        StartNewPlaythrough();
    }

    private void Update()
    {
        if (m_currentData != null)
        {
            m_currentData.UpdateTime(Time.deltaTime);
        }
    }

    public void StartNewPlaythrough()
    {
        m_currentData = new PlayerPersistentData();
    }

    public void AddData(string phoneNumber)
    {
        m_playersInfo.m_playersInfoList.Add
            (new PlayerPersistentData(phoneNumber,m_currentData.GetNumOfConsumedLives(),m_currentData.GetPlayingTime()));
    }

    public void SaveData(string enteredText)
    {
        string phoneNumber = m_textInputManager.ValidateText(enteredText);

        SetPhoneNumber(phoneNumber);
        AddData(phoneNumber);

        string jsonString = JsonUtility.ToJson(m_playersInfo,true);
        PlayerPrefs.SetString(m_playersInfoString, jsonString);
    }

    private void SetPhoneNumber(string phoneNumber)
    {
        m_currentData.SetPhoneNumber(phoneNumber);
    }

    public void RetrieveData()
    {
        
        string jsonString = PlayerPrefs.GetString(m_playersInfoString, "Empty");
        m_playersInfo = JsonUtility.FromJson<PlayersInfo>(jsonString);
    }

    private void IncrementNumOfConsumedLives()
    {
        Debug.Log("Increasing number of lives");
        m_currentData.IncrementConsumedLives();
    }
}

[Serializable]
public class PlayersInfo
{
    public List<PlayerPersistentData> m_playersInfoList = new List<PlayerPersistentData>();
}
