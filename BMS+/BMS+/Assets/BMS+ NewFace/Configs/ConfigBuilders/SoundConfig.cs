using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundConfig", menuName ="Configs/SoundConfig")]
public class SoundConfig : ScriptableObject
{
    public List<SoundData> sounds;

}
[Serializable]
public class SoundData
{
    public AudioClip audioClip;
    public GameEnums.SoundName name;

}
