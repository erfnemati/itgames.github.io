using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersistentDataManager : MonoBehaviour
{

    public static PersistentDataManager _instance;

    private TextInputManager m_textInputManager;
    public const string m_playersInfoString = "PlayersInfo";
    PlayerInfos m_playersInfo;
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
        m_playersInfo = GetComponent<PlayerInfos>();
        if (m_playersInfo != null)
        {
            Debug.Log("Null not here");
        }
        StartNewPlaythrough();
        RetrieveData();
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
        if (m_currentData != null)
        {
            Debug.Log("Not null here");
        }
    }

    public void AddData()
    {
        if (m_playersInfo == null)
        {
            Debug.Log("It is null");
        }
        //m_playersInfo.m_PlayerPersistentDatas.Add(m_currentData);
    }

    public void SaveData(string enteredText)
    {
        string phoneNumber = m_textInputManager.ValidateText(enteredText);
        if (phoneNumber == null)
        {
            Debug.Log("No need to save");
            return;
        }

        SetPhoneNumber(phoneNumber);

        AddData();

        Debug.Log("Saving player persistend data");
        string jsonString = JsonUtility.ToJson(m_playersInfo);
        PlayerPrefs.SetString(m_playersInfoString, jsonString);
        PlayerPrefs.Save();
    }

    private void SetPhoneNumber(string phoneNumber)
    {
        m_currentData.SetPhoneNumber(phoneNumber);
    }

    private void RetrieveData()
    {
        string jsonString = PlayerPrefs.GetString(m_playersInfoString, null);
        if (jsonString == null)
        {
            Debug.Log("No data");
        }

        else
        {
            m_playersInfo = JsonUtility.FromJson<PlayerInfos>(jsonString);
        }
    }

    private void IncrementNumOfConsumedLives()
    {
        if (m_currentData == null)
        {
            Debug.Log("Null here");
        }
        else
        {
            Debug.Log("Increasing number of lives");
            m_currentData.IncrementConsumedLives();
        }
    }

}
