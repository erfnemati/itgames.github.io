using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ColorConfig", menuName ="color config")]
public class ShapeColorConfig : ScriptableObject
{
    public shapeType ShapeType;
    public List<ShapeColorData> shapeColors = new List<ShapeColorData>();

}
