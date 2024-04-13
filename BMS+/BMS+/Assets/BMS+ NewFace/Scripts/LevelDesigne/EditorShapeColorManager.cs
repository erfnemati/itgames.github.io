using ConfigData;
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

        public VectorInt GetCombinedColor(VectorInt baseColor, VectorInt addedColor)
        {
            VectorInt resaultColor = baseColor + addedColor;

            Debug.Log(resaultColor);
            ShapeConfigData data = EditorDataManager._instance.GetData<ShapeConfigData>(resaultColor);
            if (data == null)
                return resaultColor;
            else
                return data.color;
        }

        public bool IsJammedCheck(VectorInt resaultColor)
        {
            if (resaultColor.f == 1)
                return true;
            else 
                return false;
        }

        public VectorInt GetSubtractedColor(VectorInt baseColor, VectorInt subtractedColor)
        {
            VectorInt resaultColor = baseColor - subtractedColor;

            Debug.Log(resaultColor);
            ShapeConfigData data = EditorDataManager._instance.GetData<ShapeConfigData>(resaultColor);
            if (data == null)
                return resaultColor;
            else
                return data.color;
        }

        public Sprite GetSprite(VectorInt color)
        {
            ShapeConfigData data;
            if (IsJammedCheck(color))
                data = EditorDataManager._instance.GetData<ShapeConfigData>(VectorInt.Jammed);
            else
                data = EditorDataManager._instance.GetData<ShapeConfigData>(color);
            if(data!=null)
                return data.sprite;
            else 
                return null;
        }  
        public GameColorName? GetColorName(VectorInt color)
        {
            ShapeConfigData data;
            if (IsJammedCheck(color))
                data = EditorDataManager._instance.GetData<ShapeConfigData>(VectorInt.Jammed);
            else
                data = EditorDataManager._instance.GetData<ShapeConfigData>(color);
            if (data!=null)
                return data.name;
            else 
                return null;
        }
    }
}