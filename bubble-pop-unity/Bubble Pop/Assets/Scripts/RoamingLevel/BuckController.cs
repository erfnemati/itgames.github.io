using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckController : MonoBehaviour
{
    private const string FUEL_BUBBLE_TAG = "FuelBubble";
    private int m_numOfAchievedBubbles;
    [SerializeField] int m_numOfNeededFuelBubbles;

    [SerializeField] BuckUiController buckUiController;

    private void Start()
    {
        SetNeededFuelBubblesUi();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(FUEL_BUBBLE_TAG))
        {
            RoamingInputHandler._instance.SetIsDragging(false);
            Destroy(collision.gameObject);
            IncreaseFuelBubble();
            UpdateUi();
            

        }
    }

    private void IncreaseFuelBubble()
    {
        m_numOfAchievedBubbles++;
        if (m_numOfAchievedBubbles == m_numOfNeededFuelBubbles)
        {
            ShuttleController._instance.StartShuttle();
        }
    }

    private void UpdateUi()
    {
        buckUiController.SetText(m_numOfAchievedBubbles);
    }

    private void SetNeededFuelBubblesUi()
    {
        buckUiController.SetNumOfNeededFuelBubbles(m_numOfNeededFuelBubbles);
    }

    

    
}
