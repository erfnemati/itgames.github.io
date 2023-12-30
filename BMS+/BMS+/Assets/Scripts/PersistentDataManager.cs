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
    }

    private void OnDisable()
    {
        LevelManager.OnLevelDefeat -= IncrementNumOfConsumedLives;
        PhoneScreenManager.EndLevel -= TurnOffTimer;
        PlayerLifeManager.GameIsOver -= GetLevel;
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
            (new PlayerPersistentData(phoneNumber,m_currentData.GetNumOfConsumedLives(),m_currentData.GetPlayingTime()));
    }

    public void SaveData(string enteredText)
    {
        string phoneNumber = m_textInputManager.ValidateText(enteredText);

        SetPhoneNumber(phoneNumber);
        AddData(phoneNumber);

        string jsonString = JsonUtility.ToJson(m_playersInfo,true);
        PlayerPrefs.SetString(m_playersInfoString, jsonString);
        RetrieveData();
    }

    private void SetPhoneNumber(string phoneNumber)
    {
        m_currentData.SetPhoneNumber(phoneNumber);
    }

    public void RetrieveData()
    {
        string jsonString = PlayerPrefs.GetString(m_playersInfoString, "Empty");
        m_playersInfo = JsonUtility.FromJson<PlayersInfo>(jsonString);

        foreach(PlayerPersistentData temp in m_playersInfo.m_playersInfoList)
        {
            m_phoneNumer = temp.GetPhoneNumber();
            m_numOfConsumedLives = temp.GetNumOfConsumedLives();
            m_passedTime = temp.GetPlayingTime();

            Debug.Log("Phone number : " + temp.GetPhoneNumber());
            Debug.Log("Consumed number of lives : " + temp.GetNumOfConsumedLives());
            Debug.Log("Passed time : " + temp.GetPlayingTime());
        }
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
