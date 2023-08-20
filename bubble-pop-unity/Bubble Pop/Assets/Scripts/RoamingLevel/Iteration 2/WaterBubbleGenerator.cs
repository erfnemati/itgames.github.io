using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBubbleGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> m_waterBubbleStates = new List<GameObject>();

    private GameObject m_currentWaterBubble = null;
    private int m_currentWaterBubbleStateIndex = 0;

    public void SetNextWaterBubbleState()
    {
        m_currentWaterBubble = m_waterBubbleStates[m_currentWaterBubbleStateIndex];
        m_currentWaterBubbleStateIndex = (m_currentWaterBubbleStateIndex + 1) % m_waterBubbleStates.Count;
    }

    public void PlayNextWaterBubbleState()
    {
        Debug.Log("Going next state");
        if (m_currentWaterBubble != null)
        {
            m_currentWaterBubble.gameObject.SetActive(false);
        }

        SetNextWaterBubbleState();
        m_currentWaterBubble.gameObject.SetActive(true);
    }
}
