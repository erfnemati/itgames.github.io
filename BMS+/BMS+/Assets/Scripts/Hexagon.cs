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
        m_colorList.Add(HexagonColor.White);
    }

    public void AddColor(HexagonColor chosenColor)
    {
        if (m_colorList.Contains(chosenColor))
        {
            Debug.Log("Repeatitive color");
            return;
        }
        m_colorList.Add(chosenColor);
        ReloadColorFromAddition(chosenColor);
    }

    public void DeleteColor(HexagonColor deletedColor)
    {
        foreach (HexagonColor hexagonColor in m_colorList)
        {
            if(hexagonColor == deletedColor)
            {
                m_colorList.Remove(hexagonColor);
                break;
            }
        }
        ReloadColorFromDeletion();
    }

    private void ReloadColorFromDeletion()
    {
        m_currentColor  = HexagonColorManager._instance.GetColor(m_colorList);
        UpdateSprite();
    }

    private void ReloadColorFromAddition(HexagonColor chosenColor)
    {
        m_currentColor = HexagonColorManager._instance.GetColor(m_currentColor, chosenColor);
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        m_spriteRenderer.sprite =  HexagonSpriteManager._instance.GetHexagonSprite(m_currentColor);
    }

    public HexagonColor GetHexagonColor()
    {
        return m_currentColor;
    }

    public void SetHexagonColor(HexagonColor hexagonColor)
    {
        m_currentColor = hexagonColor;
        UpdateSprite();
    }

    

}
