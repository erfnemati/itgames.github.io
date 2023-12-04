using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;

public class Hexagon : MonoBehaviour
{
    
    protected HexagonColor m_currentColor = HexagonColor.White;
    [SerializeField] protected List<HexagonColor> m_colorList = new List<HexagonColor>();
    private int m_numOfAddedColors = 0;
    [SerializeField] protected RTLTextMeshPro m_numberOfAddedColorsText;
    [SerializeField] protected SpriteRenderer m_spriteRenderer;

    private void Awake()
    {
        if(m_numberOfAddedColorsText == null)
        {
            Debug.Log("It is null");
        }
        //m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_colorList.Add(HexagonColor.White);
        //AddColor(HexagonColor.White);
        UpdateNumOfAddedColorsText();
        
    }

    public void AddColor(HexagonColor chosenColor)
    {
        m_colorList.Add(chosenColor);
        ReloadColorFromAddition(chosenColor);
        m_numOfAddedColors++;
        UpdateNumOfAddedColorsText();
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
        m_numOfAddedColors--;
        ReloadColorFromDeletion();
        UpdateNumOfAddedColorsText();
    }

    private void UpdateNumOfAddedColorsText()
    {
        if(m_numOfAddedColors > 0)
        {

        }
        m_numberOfAddedColorsText.text = m_numOfAddedColors + "";
        
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
        //m_spriteRenderer.sprite =  HexagonSpriteManager._instance.GetHexagonSprite(m_currentColor);
        m_spriteRenderer.color = HexagonColorPicker._instance.GetHexagonColor(m_currentColor);

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
