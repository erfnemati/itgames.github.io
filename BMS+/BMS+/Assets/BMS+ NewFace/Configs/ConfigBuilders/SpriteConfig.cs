using ConfigData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteConfig", menuName = "Configs/SpriteConfig")]
public class SpriteConfig : ScriptableObject
{
    public List<SpriteConfigData> sprites = new List<SpriteConfigData>();
}













