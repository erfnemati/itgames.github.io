using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class HexagonColorManager : MonoBehaviour
{
    public static HexagonColorManager _instance;

    [SerializeField] Sprite m_whiteHexagonColorSprite;
    [SerializeField] Sprite m_blueHexagonColorSprite;
    [SerializeField] Sprite m_redHexagonColorSprite;
    [SerializeField] Sprite m_yellowHexagonColorSprite;
    [SerializeField] Sprite m_orangeHexagonColorSprite;
    [SerializeField] Sprite m_greenHexagonColorSprite;
    [SerializeField] Sprite m_purpleHexagonColorSprite;
    [SerializeField] Sprite m_brownHexagonColorSpirte;

    private void Awake()
    {
        if(_instance == null || _instance != this)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private HexagonColor CalculateColor(List<HexagonColor> colorList)
    {
        HexagonColor currentColor = colorList[0];
        for (int i = 1; i < colorList.Count;i++)
        {
            currentColor = CalculateColor(currentColor,colorList[i]);
        }
        return currentColor;
    }

    private HexagonColor CalculateColor(HexagonColor currentColor,HexagonColor addedColor)
    {
        if (addedColor == HexagonColor.Yellow)
        {
            if (currentColor == HexagonColor.Blue)
            {
                return HexagonColor.Green;
            }
            else if (currentColor == HexagonColor.Red)
            {
                return HexagonColor.Orange;
            }

            else if (currentColor == HexagonColor.Purple)
            {
                return HexagonColor.Brown;
            }
            else if (currentColor == HexagonColor.White)
            {
                return addedColor;
            }
            else
            {
                return currentColor;
            }
        }

        else if (addedColor == HexagonColor.Blue)
        {
            if (currentColor == HexagonColor.Yellow)
            {
                return HexagonColor.Green;
            }
            else if (currentColor == HexagonColor.Red)
            {
                return HexagonColor.Purple;
            }
            else if (currentColor == HexagonColor.Orange)
            {
                return HexagonColor.Brown;
            }
            else if (currentColor == HexagonColor.White)
            {
                return addedColor;
            }
            else
            {
                return currentColor;
            }
           
        }
        else if (addedColor == HexagonColor.Red)
        {
            if (currentColor == HexagonColor.Yellow)
            {
                return HexagonColor.Orange;
            }
            else if (currentColor == HexagonColor.Blue)
            {
                return HexagonColor.Purple;
            }
            else if (currentColor == HexagonColor.Green)
            {
                return HexagonColor.Brown;
            }
            else if (currentColor == HexagonColor.White)
            {
                return addedColor;
            }
            else
            {
                return currentColor;
            }
        }
        else
        {
            return currentColor;
        }

        
    }

    public Sprite GetSprite(List<HexagonColor> colorList)
    {
        HexagonColor nextColor = CalculateColor(colorList);
        Sprite nextSprite = ChooseSprite(nextColor);
        return nextSprite;
    }

    public Sprite GetSprite(HexagonColor currentColor,HexagonColor addedColor)
    {
        HexagonColor nextColor = CalculateColor(currentColor, addedColor);
        Sprite nextSprite = ChooseSprite(nextColor);
        return nextSprite;
    }

    private Sprite ChooseSprite(HexagonColor color)
    {
        if (color == HexagonColor.White)
        {
            return m_whiteHexagonColorSprite;
        }
        else if (color == HexagonColor.Blue)
        {
            return m_blueHexagonColorSprite;
        }
        else if (color == HexagonColor.Red)
        {
            return m_redHexagonColorSprite;
        }
        else if (color == HexagonColor.Yellow)
        {
            return m_yellowHexagonColorSprite;
        }
        else if (color == HexagonColor.Orange)
        {
            return m_orangeHexagonColorSprite;
        }
        else if (color == HexagonColor.Green)
        {
            return m_greenHexagonColorSprite;
        }
        else if (color == HexagonColor.Purple)
        {
            return m_purpleHexagonColorSprite;
        }
        else if (color == HexagonColor.Brown)
        {
            return m_brownHexagonColorSpirte;              
        }
        else
        {
            return m_whiteHexagonColorSprite;
        }
    }

    
}
