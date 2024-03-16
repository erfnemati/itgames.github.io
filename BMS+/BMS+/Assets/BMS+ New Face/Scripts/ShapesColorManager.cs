using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesColorManager 
{
    public ShapesColorManager() { }

    public  Color GetCombinedColor(Color BaseColor, Color addedColor)
    {
        Color resaultColor = BaseColor+addedColor/2;
        return DataManager._instance.GetColorData<ShapeColorData>(resaultColor).color;
    }
    public Color GetSubtractedColor(Color BaseColor, Color subtractedColor)
    {
        Color resaultColor= BaseColor-subtractedColor/2;
        return DataManager._instance.GetColorData<ShapeColorData> (resaultColor).color;
    }

    public Sprite GetSprite(Color color)
    {
        return DataManager._instance.GetColorData<ShapeColorData>(color).sprite;
    }
}
