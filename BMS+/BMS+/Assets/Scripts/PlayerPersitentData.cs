[System.Serializable]
public class PlayerPersistentData
{
    private string m_phoneNumber;
    private int m_numOfConsumedLives;
    private float m_playingTime;

    public PlayerPersistentData()
    {
        SetPhoneNumber();
        SetPlayingTime();
        SetNumOfConsumedLives();
    }

    public PlayerPersistentData(string phoneNumber,int lives,float time)
    {
        SetPhoneNumber(phoneNumber);
        SetNumOfConsumedLives(lives);
        SetPlayingTime(time);
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

    public void SetPhoneNumber(string phoneNumber="Not Added Yer")
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
}
