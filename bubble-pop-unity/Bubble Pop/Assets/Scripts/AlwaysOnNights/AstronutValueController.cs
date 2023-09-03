using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronutValueController : MonoBehaviour
{
    private float m_remainingValue = 90f;
    private float m_minThreshold = -5;
    [SerializeField] float m_drainValue; //Per second

    private AstronutUiController m_astronutUiController;


    private void Start()
    {
        m_astronutUiController = GetComponent<AstronutUiController>();
    }

    private void Update()
    {
        DrainValue();
        UpdateAstronutUi();
    }
    public void AddValue(float addedValueFloat)
    {
        m_remainingValue += addedValueFloat;
        NotifyChangedValue();
    }

    private void DrainValue()
    {
        m_remainingValue -= m_drainValue * Time.deltaTime;
        NotifyChangedValue();
    }

    private void NotifyChangedValue()
    {
        AlwaysOnNigthsGameManager._instance.CheckValue(m_remainingValue);
    }


    private void UpdateAstronutUi()
    {
        m_astronutUiController.UpdateSlider(m_remainingValue);
    }

}
