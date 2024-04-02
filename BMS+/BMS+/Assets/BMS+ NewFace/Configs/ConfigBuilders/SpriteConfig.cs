using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SpriteConfig",menuName = "Configs/SpriteConfig")]
public class SpriteConfig : ScriptableObject
{
    public List<GameSpriteData> sprites = new List<GameSpriteData>();
}
[Serializable]
public class GameSpriteData
{
    public Sprite sprite;
    public  GameEnums.SpriteName name;
}
