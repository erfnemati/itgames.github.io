using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AstronutUiController : MonoBehaviour
{
    [SerializeField] float m_maxValue;
    [SerializeField] Slider m_astronutSlider;


    private void Start()
    {
        InitialiseSlider();
    }

    private void InitialiseSlider()
    {
        m_astronutSlider.maxValue = m_maxValue;
    }

    public void UpdateSlider(float value)
    {
        m_astronutSlider.value = value;
    }
    
}
