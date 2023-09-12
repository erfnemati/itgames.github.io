using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AstronutValueController : MonoBehaviour
{
    private float m_remainingValue = 5f;
    private float m_minThreshold = 0;
    private bool m_isLevelOver = false;
    private bool m_isTutorialOver = false;
    [SerializeField] float m_drainValue; //Per second
    [SerializeField] float m_maxDrainValue;
    [SerializeField] Light2D m_astronutSpotLight;
    [SerializeField] float m_maxValue;
    [SerializeField] float m_drainValueChange = 0.3f;

    private AstronutUiController m_astronutUiController;


    private void Start()
    {
        m_astronutUiController = GetComponent<AstronutUiController>();
        m_remainingValue = m_astronutSpotLight.pointLightOuterRadius;
    }

    private void Update()
    {
        if (m_isTutorialOver == false)
            return;
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

    public void EndTutorial()
    {
        m_isTutorialOver = true;
    }

    public void IncreaseDrainRate()
    {
        m_drainValue += m_drainValueChange;
        if (m_drainValue >= m_maxDrainValue)
        {
            m_drainValue = m_maxDrainValue;
        }
    }

    

}
