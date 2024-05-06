using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Newtonsoft.Json;
using System.Net.Sockets;

// [change] should get time from level Timer
public class PersistentDataManager : MonoBehaviour,IGameService
{

    public const string m_playersInfoString = "PlayersInfo";

    private EventManager eventManager;
    private ApiManager apiManager;
    public List<PlayerPersistentData> m_players;
    PlayerPersistentData m_currentData;

    private bool m_isLevelOver = false;

    [SerializeField] string m_phoneNumer;
    [SerializeField] int m_numOfConsumedLives;
    [SerializeField] float m_passedTime;
    [SerializeField] int m_playerLastLevel;

    private string m_saveDataPath;


    static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets};
    static readonly string ApplicationName = "Dakalchin";
    static readonly string SpreadsheetId = "Your SpreadSheet Id";
    static readonly string sheet = "DakalChinLeaderboard";
    private string m_sheetKeyName = "SheetKey.json";
    private string m_spreadSheetId = "1ezyZpRn0TlhcrO6R0u8J8GZViRCU5xdVkZKH1_GLUmU";

    public void Awake()
    {
        //ServiceLocator._instance.Register(this);

    }
    private void AddEvents()
    {
        eventManager.StartListening(EventName.OnLevelDefeat, new Action(IncrementNumOfConsumedLives));
        eventManager.StartListening(EventName.OnLevelRetreat, new Action(GetLevel));
        eventManager.StartListening(EventName.OnGameWin, new Action(GetLevelAfterWin));
        eventManager.StartListening(EventName.OnLevelVictory, new Action(GetLevelAfterWin));


        //LevelManager.OnLevelDefeat += IncrementNumOfConsumedLives;
        //PhoneScreenManager.EndLevel += TurnOffTimer;
        //PlayerLifeManager.GameIsOver += GetLevel;
        //LevelManager.OnLevelRetreat += GetLevel;
        //LevelManager.OnGameWin += GetLevelAfterWin;
    }

    public void OnDestroy()
    {
        eventManager.StopListening(EventName.OnLevelDefeat, new Action(IncrementNumOfConsumedLives));
        eventManager.StopListening(EventName.OnLevelRetreat, new Action(GetLevel));
        eventManager.StopListening(EventName.OnGameWin, new Action(GetLevelAfterWin));
        eventManager.StopListening(EventName.OnLevelVictory, new Action(GetLevelAfterWin));

        //LevelManager.OnLevelDefeat -= IncrementNumOfConsumedLives;
        //PhoneScreenManager.EndLevel -= TurnOffTimer;
        //PlayerLifeManager.GameIsOver -= GetLevel;
        //LevelManager.OnLevelRetreat -= GetLevel;
        //LevelManager.OnGameWin -= GetLevelAfterWin;

    }
    public void Start()
    {
        eventManager = ServiceLocator._instance.Get<EventManager>();
        apiManager = ServiceLocator._instance.Get<ApiManager>();
        AddEvents();
        Debug.Log("Instantiating new playersInfo object");
        m_saveDataPath = Application.dataPath + "LeaderBoardTextFile.json";
        StartNewPlaythrough();
        RetrieveData();

        //Debug.Log(GetSpreadsheet(m_spreadSheetId).SpreadsheetUrl);
    }


    public void StartNewPlaythrough()
    {
        m_currentData = new PlayerPersistentData();
    }

    public void AddData(string phoneNumber)
    {
        m_players.Add
            (new PlayerPersistentData
                (phoneNumber,m_currentData.GetNumOfConsumedLives(),m_currentData.GetPlayingTime(),
                m_currentData.GetPlayerLastLevel())
            );
    }

    public void SaveData(string enteredText)
    {
        SetPhoneNumber(enteredText);
        m_players.Add(m_currentData);
        //AddData(enteredText);
        m_players.Sort();
        string jsonData =  JsonConvert.SerializeObject(m_currentData);
        Debug.Log(jsonData);
        apiManager.CallApi(Apis.addLeaderboardData, ApiManager.NetworkMethod.Post, data: jsonData);

        //Debug.Log("Here is saved note: " + jsonString);
        //File.WriteAllText(Application.dataPath + "LeaderBoardTextFile.json", jsonString);
        string jsonAllData = JsonConvert.SerializeObject(m_players);
        PlayerPrefs.SetString(m_playersInfoString, jsonAllData);
        PlayerPrefs.Save();
    }

    private void SetPhoneNumber(string phoneNumber)
    {
        m_currentData.SetPhoneNumber(phoneNumber);
        Debug.Log(phoneNumber);

    }

    public void RetrieveData()
    {
      apiManager.CallApi(Apis.fetchLeaderBoard, ApiManager.NetworkMethod.Get, SetPlayerInfos);

    }
    void SetPlayerInfos(string data)
    {
        if(data !=null)
            m_players = JsonConvert.DeserializeObject<List<PlayerPersistentData>>(data);
        else
            if(PlayerPrefs.HasKey(m_playersInfoString))
                m_players = JsonConvert.DeserializeObject<List<PlayerPersistentData>>(PlayerPrefs.GetString(m_playersInfoString));
            else
                m_players= new List<PlayerPersistentData>();

    }
    //public void RetrieveData()
    //{
    //    //string jsonString = PlayerPrefs.GetString(m_playersInfoString, "Empty");
    //    Debug.Log("Retriving");
    //    if (File.Exists(m_saveDataPath))
    //    {
    //        string retrivedJsonString = File.ReadAllText(m_saveDataPath);
    //        Debug.Log(retrivedJsonString);
    //        if (retrivedJsonString != null)
    //        {
    //            m_playersInfo = JsonUtility.FromJson<PlayersInfo>(retrivedJsonString);
    //        }
    //        else
    //        {
    //            Debug.Log("Nothing to show for now");
    //        }

    //    }
    //}

    public void IncrementNumOfConsumedLives()
    {
        Debug.Log("Increasing number of lives");
        m_currentData.IncrementConsumedLives();
    }


    public PlayerPersistentData GetCurrentPlayerData()
    {
        return m_currentData;
    }

    public void GetLevel()
    {
        Debug.Log("Getting Level");
        int level = ServiceLocator._instance.Get<BmsPlusSceneManager>().GetCurrentLevel();
        m_currentData.SetPlayerLastLevel(level -1);
        m_playerLastLevel = level -1;
    }

    public void GetLevelAfterWin()
    {
        int level = ServiceLocator._instance.Get<BmsPlusSceneManager>().GetCurrentLevel();
        m_currentData.SetPlayerLastLevel(level);
        m_playerLastLevel = level;
        Debug.Log("Our level is : " + level);
    }


    public int GetIndex(int id)
    {
        for (int i = 0; i < m_players.Count; i++)
        {
            if (m_players[i].GetPlayerId() == id)
            {
                return i;
            }

        }
        return -1;
    }
    public float  GetPlayerCluster()
    {
        if (m_players.Count <= 4)
        {
            if (m_currentData.GetPlayerLastLevel() >= 11 && m_currentData.GetPlayerLastLevel() <= 14)
            {
                return 0.125f;
            }

            else
            {
                return 0.3f;
            }
        }

        int playerRank = GetIndex(m_currentData.GetPlayerId()) + 1;
        float playerCluster = (float)playerRank / m_players.Count;
        Debug.Log("Player cluster is : " + playerCluster);
        return playerCluster;
        //Debug.Log("Player rank is : " + m_playersInfo.GetIndex(m_currentData.GetPlayerId()));
        //return (m_playersInfo.GetIndex(m_currentData.GetPlayerId()));
    }

    public List<PlayerPersistentData> GetPlayersInfo()
    {
        return m_players;
    }

    public Spreadsheet CreateSpreadsheet(string title)
    {
        var googleCredential = GetCredential();
        if (googleCredential == null)
            throw new ArgumentNullException();

        var service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = googleCredential,
            ApplicationName = ApplicationName,
        });

        Spreadsheet chosenSpreadSheet = new Spreadsheet()
        {
            Properties = new SpreadsheetProperties()
            {
                Title = title
            }
        };

        return (service.Spreadsheets.Create(chosenSpreadSheet).Execute());
    }


    public GoogleCredential GetCredential()
    {
        if (File.Exists(Application.dataPath + m_sheetKeyName))
        {
            string keyPath = Application.dataPath + m_sheetKeyName;
            using (var stream = new FileStream(keyPath, FileMode.Open, FileAccess.Read))
            {
                return (GoogleCredential.FromStream(stream).CreateScoped(Scopes));
            }
        }
        return null;
    }

    public Spreadsheet GetSpreadsheet(string spreadsheetId)
    {

        var googleCredential = GetCredential();
        if (googleCredential == null)
            throw new ArgumentNullException();

        var service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = googleCredential,
            ApplicationName = ApplicationName,
        });

        return (service.Spreadsheets.Get(spreadsheetId).Execute());
    }
}

[Serializable]
public class PlayersInfo
{
    public List<PlayerPersistentData> m_playersInfoList;
    public PlayersInfo()
    {
        m_playersInfoList= new List<PlayerPersistentData>();

    }

    public  void Sort()
    {
        m_playersInfoList.Sort();
    }

    public int GetIndex(int id)
    {
        for(int i = 0; i < m_playersInfoList.Count; i++)
        {
            if (m_playersInfoList[i].GetPlayerId() == id)
            {
                return i;
            }
                
        }
        return -1;
    }
}
