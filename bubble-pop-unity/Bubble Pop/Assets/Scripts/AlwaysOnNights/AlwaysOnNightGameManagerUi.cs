using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using RTLTMPro;

public class AlwaysOnNightGameManagerUi : MonoBehaviour
{
    //[SerializeField] Slider m_passedTimeSlider;
    [SerializeField] RTLTextMeshPro m_remainingTime;


    public void InitialiseSlider(float maxValue)
    {
        m_remainingTime.text = (int)maxValue + "";
    }

    public void UpdateSlider(float value)
    {
        m_remainingTime.text = (int)value + "";
    }
}
