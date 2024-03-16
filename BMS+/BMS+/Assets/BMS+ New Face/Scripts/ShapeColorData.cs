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
    private string _name;
    public string name { get { return _name; }}
    [SerializeField]
    private int _id;
    public int id { get { return _id; }}
    [SerializeField]
    private Sprite _sprite;
    public Sprite sprite { get { return _sprite; }}
    [SerializeField]


    public ShapeColorData(Color color,string name,int id, Sprite sprite)
    {
        this._color = color;
        this._name = name;
        this._id = id;
        this._sprite = sprite;

    }
}

