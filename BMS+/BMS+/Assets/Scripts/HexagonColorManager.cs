using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class HexagonColorManager : MonoBehaviour
{
    public static HexagonColorManager _instance;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
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

    public HexagonColor GetColor(List<HexagonColor> colorList)
    {
        HexagonColor nextColor = CalculateColor(colorList);
        return nextColor;
    }

    public HexagonColor GetColor(HexagonColor currentColor,HexagonColor addedColor)
    {
        HexagonColor nextColor = CalculateColor(currentColor, addedColor);
        return nextColor;
    }

    

    
}
