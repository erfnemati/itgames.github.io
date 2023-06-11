using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSlider : MonoBehaviour
{

    [SerializeField] int m_maxValue;
    [SerializeField] Slider m_slider;

    [SerializeField] Gradient m_sliderGradient;
    [SerializeField] Image fillBar;
    // Start is called before the first frame update
    void Start()
    {
        SetValue(0);
    }

    public void SetValue(float newValue)
    {
        if (newValue < 0)
        {
            newValue = 0;
        }
        m_slider.value = newValue;
        fillBar.color = m_sliderGradient.Evaluate(m_slider.value/m_slider.maxValue);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SetValue(m_slider.value + 0.2f);
        }
    }


    private void SetMaxValue()
    {
        m_slider.maxValue = m_maxValue;
        m_slider.value = m_maxValue;
    }
}
