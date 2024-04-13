using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GuideCanvasConfig", menuName = "Configs/GuidCanvasConfig")]
public class GuideCanvasConfig : ScriptableObject
{
    public List<ConfigData.GuideCanvasConfigData> guideCanvases;
}
