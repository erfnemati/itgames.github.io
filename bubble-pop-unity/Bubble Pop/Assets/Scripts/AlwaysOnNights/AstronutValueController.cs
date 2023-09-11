using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AstronutValueController : MonoBehaviour
{
    private float m_remainingValue = 5f;
    private float m_minThreshold = 0;
    private bool m_isLevelOver = false;
    [SerializeField] float m_drainValue; //Per second
    [SerializeField] Light2D m_astronutSpotLight;
    [SerializeField] float m_maxValue;

    private AstronutUiController m_astronutUiController;


    private void Start()
    {
        m_astronutUiController = GetComponent<AstronutUiController>();
        m_remainingValue = m_astronutSpotLight.pointLightOuterRadius;
    }

    private void Update()
    {
        if (m_isLevelOver)
            return;

        DrainValue();
    }
    public void AddValue(float addedValueFloat)
    {
        m_remainingValue += addedValueFloat;
        if (m_remainingValue > m_maxValue)
        {
            m_remainingValue = m_maxValue;
        }
        NotifyChangedValue();
    }

    private void DrainValue()
    {
        m_remainingValue -= m_drainValue * Time.deltaTime;
        m_astronutSpotLight.pointLightOuterRadius = m_remainingValue;
        NotifyChangedValue();
    }

    private void NotifyChangedValue()
    {
        AlwaysOnNigthsGameManager._instance.CheckValue(m_remainingValue);
    }

    public void EndLevel()
    {
        m_isLevelOver = true;
    }

}
