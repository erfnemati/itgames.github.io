using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AstronutUiController : MonoBehaviour
{
    [SerializeField] Sprite m_goingRight;
    [SerializeField] Sprite m_goingLeft;

    private SpriteRenderer m_spriteRenderer;


    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
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
