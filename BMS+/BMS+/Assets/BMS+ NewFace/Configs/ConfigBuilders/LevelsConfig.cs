using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelsConfig",menuName ="Configs/LevelsConfig")]
public class LevelsConfig : ScriptableObject
{
    public List<LevelConfig> levels;
}
