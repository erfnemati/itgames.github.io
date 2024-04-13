using GameEnums;
using System;
using UnityEngine;

namespace ConfigData
{
    [Serializable]
    public class PinConfigData
    {
        [SerializeField]
        private VectorInt _color;
        public VectorInt color { get { return _color; } }
        [SerializeField]
        private PinName _name;
        public PinName name { get { return _name; } }

        [SerializeField]
        private Sprite _sprite;
        public Sprite sprite { get { return _sprite; } }


        public PinConfigData(PinName name, VectorInt color, Sprite sprite)
        {
            this._name = name;
            this._color = color;
            this._sprite = sprite;
        }
    }

    [Serializable]
    public class ShapeConfigData
    {
        [SerializeField]
        private VectorInt _color;
        public VectorInt color { get { return _color; } }
        [SerializeField]
        private GameColorName _name;
        public GameColorName? name { get { return _name; } }
        [SerializeField]
        private Sprite _sprite;
        public Sprite sprite { get { return _sprite; } }
        [SerializeField]


        public ShapeConfigData(VectorInt color, GameColorName name, Sprite sprite)
        {
            this._color = color;
            this._name = name;
            this._sprite = sprite;

        }
    }

    [Serializable]
    public class SpriteConfigData

    {
        public Sprite sprite;
        public GameEnums.SpriteName name;
    }

    [Serializable]
    public class SoundConfigData
    {
        public AudioClip audioClip;
        public GameEnums.SoundName name;

    }

    [Serializable]
    public class GuideCanvasConfigData
    {
        public GuideCanvasName name;
        public GameObject prefab;
    }


}
