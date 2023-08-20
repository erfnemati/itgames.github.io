using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;

public class BuckUiController : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro m_RtlTextMeshPro;
    private int m_numOfNeededFuelBubbles;

    public void SetText(int numberOfAchievedBubbles = 0)
    {
        m_RtlTextMeshPro.text =  m_numOfNeededFuelBubbles  + "/ " + numberOfAchievedBubbles;
    }

    private void Start()
    {
        SetText(0);
    }

    public void SetNumOfNeededFuelBubbles(int neededFuelBubble)
    {
        m_numOfNeededFuelBubbles = neededFuelBubble;
        SetText();
        
    }
}
