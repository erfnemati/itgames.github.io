using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShuttleUiControllerSecIteration : MonoBehaviour
{
    [SerializeField] Slider m_FuelSilder;
    [SerializeField] GameObject m_fireEngineGameObject;
    [SerializeField] Sprite m_startedShuttleSprite;

    public void SetSliderMaxValue(int maxValue)
    {
        m_FuelSilder.maxValue = maxValue;
    }
    public void UpdateSlider(int value)
    {
        m_FuelSilder.value = value;
    }

    public void DeactivateUiComponent()
    {
        m_FuelSilder.gameObject.SetActive(false);
    }

    public void GoToShuttle()
    {
        GetComponent<SpriteRenderer>().sprite = m_startedShuttleSprite;
        Invoke(nameof(StartEngine), 1f);
        
    }

    private void StartEngine()
    {
        m_fireEngineGameObject.gameObject.SetActive(true);
    }
}
