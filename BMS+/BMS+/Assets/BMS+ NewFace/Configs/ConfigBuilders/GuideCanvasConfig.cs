using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEnums;
using System;
[CreateAssetMenu(fileName ="GuideCanvasConfig",menuName ="Configs/GuidCanvasConfig")]
public class GuideCanvasConfig : ScriptableObject
{
    public List<GuideCanvasDataa> guideCanvases;
}

[Serializable]
public class GuideCanvasDataa
{
    public GuideCanvasName name;
    public GameObject prefab;
}
