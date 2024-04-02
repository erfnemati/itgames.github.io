using GameEnums;
using LevelDesign;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{

    public class EditorShapesColorManager
    {
        public EditorShapesColorManager() { }

        public Color GetCombinedColor(Color BaseColor, Color addedColor)
        {
            Color resaultColor = BaseColor + addedColor ;

            ShapeColorData data = EditorDataManager._instance.GetData<ShapeColorData>(resaultColor);
            if (data == null)
                return resaultColor;
            else
                return data.color;
        }

        private Color IsJammedCheck(Color resaultColor)
        {
            Color clampedColor = new Color(Math.Min(1, resaultColor.r), Math.Min(1, resaultColor.g),
                Math.Min(1, resaultColor.b), Math.Min(1, resaultColor.a));
            if (clampedColor == Color.white)
                return clampedColor;
            else return resaultColor;
        }

        public Color GetSubtractedColor(Color BaseColor, Color subtractedColor)
        {
            Color resaultColor = BaseColor - subtractedColor ;
            Debug.Log(resaultColor);
            ShapeColorData data = EditorDataManager._instance.GetData<ShapeColorData>(resaultColor);
            if (data == null)
                return resaultColor;
            else
                return data.color;
        }

        public Sprite GetSprite(Color color)
        {
            color = IsJammedCheck(color);
            ShapeColorData data = EditorDataManager._instance.GetData<ShapeColorData>(color);
            if(data!=null)
                return data.sprite;
            else 
                return null;
        }  
        public GameColorName? GetColorName(Color color)
        {
            ShapeColorData data = EditorDataManager._instance.GetData<ShapeColorData>(color);
            if(data!=null)
                return data.name;
            else 
                return null;
        }
    }
}