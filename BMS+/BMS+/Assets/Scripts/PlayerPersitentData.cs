using System;
using UnityEngine;

[Serializable]
public class PlayerPersistentData
{
    [SerializeField] private string m_phoneNumber;
    [SerializeField] private int m_numOfConsumedLives;
    [SerializeField] private float m_playingTime;
    [SerializeField] private int m_playerLastLevel;
    

    public PlayerPersistentData()
    {
        SetPhoneNumber();
        SetPlayingTime();
        SetNumOfConsumedLives();
        SetPlayerLastLevel();
    }

    public PlayerPersistentData(string phoneNumber,int lives,float time,int lastLevel)
    {
        SetPhoneNumber(phoneNumber);
        SetNumOfConsumedLives(lives);
        SetPlayingTime(time);
        SetPlayerLastLevel(lastLevel);
    }

    public string GetPhoneNumber()
    {
        return m_phoneNumber;
    }

    public int GetNumOfConsumedLives()
    {
        return m_numOfConsumedLives;
    }

    public float GetPlayingTime()
    {
        return m_playingTime;
    }

    public void SetPhoneNumber(string phoneNumber="Not Added Yet")
    {
        m_phoneNumber = phoneNumber;
    }

    public void SetNumOfConsumedLives(int lives=0)
    {
        m_numOfConsumedLives = lives;
    }

    public void SetPlayingTime(float time=0)
    {
        m_playingTime = time;
    }

    public void IncrementConsumedLives()
    {
        m_numOfConsumedLives += 1;
    }

    public void UpdateTime(float deltaTime)
    {
        m_playingTime += deltaTime;
    }

    public void SetPlayerLastLevel(int level=0)
    {
        m_playerLastLevel = level;
    }

    public int GetPlayerLastLevel()
    {
        return m_playerLastLevel;
    }
}
