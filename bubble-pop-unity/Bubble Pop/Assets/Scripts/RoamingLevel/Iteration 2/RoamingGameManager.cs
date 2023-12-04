using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingGameManager : MonoBehaviour
{
    [SerializeField] FuelBubbleGenerator m_fuelBubbleGenerator;
    [SerializeField]  WaterBubbleGenerator m_waterBubbleGenerator;
    [SerializeField] RoamingAstronut m_roamingAstronut;
    [SerializeField] ShuttleControllerSecIteration m_shuttleController;

    private bool m_isLevelOver = false;

    private void Start()
    {
        LoadNextState();
    }


    public void EndLevel()
    {
        m_isLevelOver = true;
        m_waterBubbleGenerator.RemoveLastWaterBubbleState();
        m_roamingAstronut.GoToShuttle();
    }

    public void StartShuttle()
    {
        m_shuttleController.StartShuttle();
    }
    public void LoadNextState()
    {
        if (m_isLevelOver == false)
        {
            m_fuelBubbleGenerator.InstansiateFuelBubble();
            m_waterBubbleGenerator.RemoveLastWaterBubbleState();
            m_waterBubbleGenerator.PlayNextWaterBubbleState();
        }
    }

    public void ReloadState()
    {
        m_fuelBubbleGenerator.InstansiateFuelBubble();
        m_waterBubbleGenerator.RestartWaterBubbleState();
    }
}
