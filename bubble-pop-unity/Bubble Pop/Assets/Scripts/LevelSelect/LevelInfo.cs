using System;

public class LevelInfo
{
	float m_elapsedTime;
	int m_recievedStars;
	bool m_isPassed;

	public LevelInfo()
	{
		m_elapsedTime = 0.0f;
		m_recievedStars = 0;
		m_isPassed = false;
	}

	public LevelInfo(int numOfStars,bool isLevelPassed)
    {
		m_elapsedTime = 0.0f;
		m_recievedStars = numOfStars;
		m_isPassed = isLevelPassed;
    }

	public int GetNumOfStars()
    {
		return m_recievedStars;
    }

	public bool GetLockedState()
    {
		return m_isPassed;
    }
}
