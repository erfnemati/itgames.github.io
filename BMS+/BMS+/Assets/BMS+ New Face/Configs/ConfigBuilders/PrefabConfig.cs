using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabConfig", menuName ="Prefab Config")]
public class PrefabConfig : ScriptableObject
{
    public GameObject ShapePrefab;
    public GameObject PinPrefab;
    public GameObject PinPointPrefab;

}
