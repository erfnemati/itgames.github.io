using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    SpriteRenderer m_spriteRenderer;
    private HexagonColor m_currentColor = HexagonColor.White;
    List<HexagonColor> m_colorList = new List<HexagonColor>();

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void AddColor()
    {
        ReloadColor();
    }

    private void DeleteColor()
    {
        ReloadColor();
    }

    private void ReloadColor()
    {
        Sprite sprite = HexagonColorManager._instance.GetSprite(m_colorList);
    }

    

}
