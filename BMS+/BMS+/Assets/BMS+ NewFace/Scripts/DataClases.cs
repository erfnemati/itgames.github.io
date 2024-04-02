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
        public Color pincolor;
    }
    [Serializable]
    public class ShapeData: IPlaceable
    {
        public int shapeId;
        public Vector3 Position;
        public Color ColorData;
        public int shapeAddedNumber;
    }

    [Serializable]
    public class PinPointData:IPlaceable
    {
        public Vector3 position;
        public Color pinPointColor;
        public PinData pinData; // get the pin from color and reset the number of pins
        public Color InitialColor;
        public List<int> neighborShapes;
    }

    [Serializable]
    public class GuideCanvasData: IPlaceable
    {
        public Vector3 position;
        public GameObject GuidCanvasPrefab;
    }

    [Serializable]
    public class EventData
    {
        public float time;
        public int shapeId;
        public Color changeToColor;
    }

}
