using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ColorConfig", menuName = "Configs/ShapeConfig")]
public class ShapeColorConfig : ScriptableObject
{
    public GameEnums.ShapeType ShapeType;
    public List<ShapeColorData> shapeColors = new List<ShapeColorData>();

}
