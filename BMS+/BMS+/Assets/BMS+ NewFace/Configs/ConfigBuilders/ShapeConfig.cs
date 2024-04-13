using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorConfig", menuName = "Configs/ShapeConfig")]
public class ShapeConfig : ScriptableObject
{
    public GameEnums.ShapeType ShapeType;
    public List<ConfigData.ShapeConfigData> shapeColors;

}

