using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] GameObject m_leaderboardElement;
    [SerializeField] GameObject m_leaderboardObject;
    [SerializeField] int m_numOfLeaderboardItems = 10;

    private void OnEnable()
    {
        Invoke(nameof(LoadLeaderBoard),0.1f);
    }

    private void LoadLeaderBoard()
    {
        if (PersistentDataManager._instance != null)
        {
            List<PlayerPersistentData> playerPersistentDatas = PersistentDataManager._instance.GetPlayersInfo();
         
            Vector3 initialLocalPos = m_leaderboardElement.GetComponent<RectTransform>().localPosition;
            float leaderboardElementHeight = m_leaderboardElement.GetComponent<RectTransform>().rect.height;
            for (int i = 0; i < playerPersistentDatas.Count;i++)
            {
                int rank = i + 1;
                string phoneNumber = playerPersistentDatas[i].GetPhoneNumber();
                int passedTime = (int)playerPersistentDatas[i].GetPlayingTime();
                int lastPassedLevel = playerPersistentDatas[i].GetPlayerLastLevel();
                
                GameObject temp = Instantiate(m_leaderboardElement,m_leaderboardObject.transform);
                temp.GetComponent<LeaderBoardElementHandler>().SetElements(rank, phoneNumber, lastPassedLevel, passedTime);
                temp.transform.localPosition = new Vector3
                    (initialLocalPos.x,
                    initialLocalPos.y - (leaderboardElementHeight * i),
                    initialLocalPos.z);
                temp.gameObject.SetActive(true);
            }
        }
    }
}
