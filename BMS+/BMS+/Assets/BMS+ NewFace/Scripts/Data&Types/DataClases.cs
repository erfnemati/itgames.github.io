using GameEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{

    public interface IPlaceable
    {

    }
    [Serializable]
    public class PinData
    {
        public int pinCapacity;
        public VectorInt pincolor;
    }
    [Serializable]
    public class ShapeData: IPlaceable, ICloneable
    {
        public int shapeId;
        public Vector3 Position;
        public VectorInt ColorData;
        public int shapeAddedNumber;
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    [Serializable]
    public class PinPointData:IPlaceable
    {
        public Vector3 position;
        public VectorInt pinPointColor;
        public PinData pinData; // get the pin from color and reset the number of pins
        public Color InitialColor;
        public List<int> neighborShapes;
    }

    [Serializable]
    public class GuideCanvasData: IPlaceable
    {
        public Vector3 position;
        public GuideCanvasName name;
    }

    [Serializable]
    public class EventData
    {
        public float time;
        public int shapeId;
        public VectorInt changeToColor;
        public int shapeAddedNumber;
    }
    [Serializable]
    public class TransformData:IPlaceable
    {
        public Vector3 boardLocation;
        public Vector3 boardScale;
    }

    [Serializable]
    public class boardData<T>:TransformData
    {
        public List<T> shapes;
    }
}
