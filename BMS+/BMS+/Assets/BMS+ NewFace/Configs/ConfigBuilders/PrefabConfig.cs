using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabConfig", menuName = "Configs/PrefabConfig")]
public class PrefabConfig : ScriptableObject
{
    public GameObject ShapePrefab;
    public GameObject PinPrefab;
    public GameObject PinPointPrefab;

}
