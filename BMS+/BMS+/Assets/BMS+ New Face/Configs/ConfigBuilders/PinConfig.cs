using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PinsConfig", menuName = "pins config")]
public class PinConfig : ScriptableObject
{
    public List<PinColorData> pins;
}

[Serializable]
public class PinColorData
{
    [SerializeField]
    private Color _color;
    public Color color { get { return _color; } }
    [SerializeField]
    private string _name;
    public string name { get { return _name; } }
    [SerializeField]
    private int _id;
    public int id { get { return _id; } }
    [SerializeField]
    private Sprite _sprite;
    public Sprite sprite { get { return _sprite; } }


    public PinColorData(string name, int id, Color color,Sprite sprite)
    {
        this._name = name;
        this._id = id;
        this._color = color;
        this._sprite=sprite;
    }
}