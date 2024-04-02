using System;
using GameEnums;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PinsConfig", menuName = "Configs/PinsConfig")]
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
    private PinName _name;
    public PinName name { get { return _name; } }

    [SerializeField]
    private Sprite _sprite;
    public Sprite sprite { get { return _sprite; } }


    public PinColorData(PinName name, Color color,Sprite sprite)
    {
        this._name = name;
        this._color = color;
        this._sprite=sprite;
    }
}