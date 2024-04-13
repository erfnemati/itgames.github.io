using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PinsConfig", menuName = "Configs/PinsConfig")]
public class PinConfig : ScriptableObject
{
    public List<ConfigData.PinConfigData> pins;
}