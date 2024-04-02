using GameEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShapeColorData
{
    [SerializeField]
    private Color _color;
    public Color color{ get { return _color; }}
    [SerializeField]
    private GameColorName _name;
    public GameColorName? name { get { return _name; }}
    [SerializeField]
    private Sprite _sprite;
    public Sprite sprite { get { return _sprite; }}
    [SerializeField]


    public ShapeColorData(Color color,GameColorName name, Sprite sprite)
    {
        this._color = color;
        this._name = name;
        this._sprite = sprite;

    }
}

