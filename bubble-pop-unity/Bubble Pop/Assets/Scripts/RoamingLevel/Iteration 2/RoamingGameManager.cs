using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingGameManager : MonoBehaviour
{
    [SerializeField] FuelBubbleGenerator m_fuelBubbleGenerator;
    [SerializeField]  WaterBubbleGenerator m_waterBubbleGenerator;

    private void Start()
    {
        LoadNextState();
    }

    public void LoadNextState()
    {
        m_fuelBubbleGenerator.InstansiateFuelBubble();
        m_waterBubbleGenerator.PlayNextWaterBubbleState();

    }
}
