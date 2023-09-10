using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AstronutUiController : MonoBehaviour
{
    [SerializeField] float m_maxValue;
    [SerializeField] Slider m_astronutSlider;
    [SerializeField] Sprite m_goingRight;
    [SerializeField] Sprite m_goingLeft;

    private SpriteRenderer m_spriteRenderer;


    private void Start()
    {
        InitialiseSlider();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void InitialiseSlider()
    {
        m_astronutSlider.maxValue = m_maxValue;
    }

    public void UpdateSlider(float value)
    {
        m_astronutSlider.value = value;
    }

    public void GoingRight()
    {
        m_spriteRenderer.sprite = m_goingRight;
    }

    public void GoingLeft()
    {
        m_spriteRenderer.sprite = m_goingLeft;
    }
    
}
