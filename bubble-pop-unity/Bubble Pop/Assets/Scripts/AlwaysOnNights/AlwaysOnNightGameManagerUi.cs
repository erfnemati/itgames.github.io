using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AlwaysOnNightGameManagerUi : MonoBehaviour
{
    [SerializeField] Slider m_passedTimeSlider;

    public void InitialiseSlider(float maxValue)
    {
        m_passedTimeSlider.maxValue = maxValue;
    }

    public void UpdateSlider(float value)
    {
        m_passedTimeSlider.value = value;
    }
}
