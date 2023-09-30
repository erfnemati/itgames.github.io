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
   // [SerializeField] Image timebar;
   // float maxtimeleft;
    private void Start()
    {
    //    timebar = GetComponent<Image>();
       
    }
   
    public void InitialiseSlider(float maxValue)
    {
      //  maxtimeleft = maxValue;
        m_remainingTime.text = (int)maxValue + "";
    }

    public void UpdateSlider(float value)
    {
      //  timebar.fillAmount = value / maxtimeleft;
       // m_remainingTime.text = (int)value + "";
    }
}
