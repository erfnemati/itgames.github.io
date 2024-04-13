using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundConfig", menuName = "Configs/SoundConfig")]
public class SoundConfig : ScriptableObject
{
    public List<ConfigData.SoundConfigData> sounds;

}
