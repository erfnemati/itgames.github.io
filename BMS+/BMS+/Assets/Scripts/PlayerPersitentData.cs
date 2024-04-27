using System;
using UnityEngine;


[Serializable]
public class PlayerPersistentData:IComparable
{
    [SerializeField] private int playerId;
    [SerializeField] public string phoneNumber;
    [SerializeField] public int numberOfConsumedLives;
    [SerializeField] public float playTime;
    [SerializeField] public int playerLastLevel;
    

    public PlayerPersistentData()
    {
        SetPhoneNumber();
        SetPlayingTime();
        SetNumOfConsumedLives();
        SetPlayerLastLevel();
        //SetPlayerId();
    }

    public PlayerPersistentData(string phoneNumber,int lives,float time,int lastLevel,int id=-1)
    {
        SetPhoneNumber(phoneNumber);
        SetNumOfConsumedLives(lives);
        SetPlayingTime(time);
        SetPlayerLastLevel(lastLevel);
        //SetPlayerId(id);
    }

    public string GetPhoneNumber()
    {
        return phoneNumber;
    }

    public int GetNumOfConsumedLives()
    {
        return numberOfConsumedLives;
    }

    public float GetPlayingTime()
    {
        return playTime;
    }

    public void SetPhoneNumber(string phoneNumber="Not Added Yet")
    {
        this.phoneNumber = phoneNumber;
    }

    public void SetNumOfConsumedLives(int lives=0)
    {
        numberOfConsumedLives = lives;
    }

    public void SetPlayingTime(float time=0)
    {
        playTime = time;
    }

    public void IncrementConsumedLives()
    {
        numberOfConsumedLives += 1;
    }

    public void UpdateTime(float deltaTime)
    {
        playTime += deltaTime;
    }

    public void SetPlayerLastLevel(int level=0)
    {
        playerLastLevel = level;
    }

    public int GetPlayerLastLevel()
    {
        return playerLastLevel;
    }

    public int GetPlayerId()
    {
        return playerId;
    }

    private void SetPlayerId(int id = -1)
    {
        if (id == -1)
        {
            var random = new System.Random();
            playerId = random.Next(1000000, 2000000);
        }
        else
        {
            playerId = id;
        }
        
    }

    public int CompareTo(object obj)
    {
        PlayerPersistentData playerPersitentData = (PlayerPersistentData)obj;
        if (playerPersitentData != null)
        {
            if (CompareWithLevel(playerPersitentData) == 0)
            {
                return CompareWithPlayingTime(playerPersitentData);
            }
            else
            {
                return CompareWithLevel(playerPersitentData);
            }
        }

        else
        {
            return 0;
        }
    }

    private int CompareWithLevel(PlayerPersistentData playerPersistentData)
    {
        if (playerPersistentData.playerLastLevel > this.playerLastLevel)
        {
            return 1;
        }
        else if (playerPersistentData.playerLastLevel < this.playerLastLevel)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    private int CompareWithPlayingTime(PlayerPersistentData playerPersistentData)
    {
        if (playerPersistentData.playTime < this.playTime)
        {
            return 1;
        }
        else if (playerPersistentData.playTime > this.playTime)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    
}
