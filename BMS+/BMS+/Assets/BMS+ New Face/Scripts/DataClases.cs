using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public PinData pinData;
    public List<int> neighborShapes;
}

[Serializable]
public class GuidCanvasData: IPlaceable
{
    public Vector3 position;
    public GameObject GuidCanvasPrefab;
}
public class DataClases 
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
