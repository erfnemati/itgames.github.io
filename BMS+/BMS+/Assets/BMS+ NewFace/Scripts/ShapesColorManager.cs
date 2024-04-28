using ConfigData;
using GameEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesColorManager
{
    private DataManager dataManager;
    public ShapesColorManager() { 
        dataManager = ServiceLocator._instance.Get<DataManager>();
    }

    public VectorInt GetCombinedColor(VectorInt baseColor, VectorInt addedColor)
    {
        VectorInt resaultColor = baseColor + addedColor;

        ShapeConfigData data = dataManager.GetData<ShapeConfigData>(resaultColor);
        if (data == null)
            return resaultColor;
        else
            return data.color;
    }

    public bool IsJammedCheck(VectorInt resaultColor)
    {
        if (resaultColor.f != 0)
            return true;
        else
            return false;
    }

    public VectorInt GetSubtractedColor(VectorInt baseColor, VectorInt subtractedColor)
    {
        VectorInt resaultColor = baseColor - subtractedColor;

        Debug.Log(resaultColor);
        ShapeConfigData data = dataManager.GetData<ShapeConfigData>(resaultColor);
        if (data == null)
            return resaultColor;
        else
            return data.color;
    }

    public Sprite GetSprite(VectorInt color)
    {
        ShapeConfigData data;
        color=VectorInt.GetLerpValue(color);
        if (IsJammedCheck(color))
            data = dataManager.GetData<ShapeConfigData>(VectorInt.Jammed);
        else
            data = dataManager.GetData<ShapeConfigData>(color);
        if (data != null)
            return data.sprite;
        else
            return null;
    }
    public GameColorName? GetColorName(VectorInt color)
    {
        ShapeConfigData data;
        color = VectorInt.GetLerpValue(color);
        if (IsJammedCheck(color))
            data = dataManager.GetData<ShapeConfigData>(VectorInt.Jammed);
        else
            data = dataManager.GetData<ShapeConfigData>(color);
        if (data != null)
            return data.name;
        else
            return null;
    }
}
