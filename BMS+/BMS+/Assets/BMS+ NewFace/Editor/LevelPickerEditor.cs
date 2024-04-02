using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelPickerEditor : ExtendedEditorWindow
{
    AnimationCurve curveX = AnimationCurve.Linear(0, 0, 10, 10);
    private LevelsConfig m_LevelsConfig;

    [MenuItem("Window/Level Picker Window")]
    public static void OpenEditor(LevelsConfig dataObject)
    {
        LevelPickerEditor window =GetWindow<LevelPickerEditor>("Level Picker Editor");
        window.m_serializedObject=new SerializedObject(dataObject);
        window.m_LevelsConfig = dataObject;
    }
    private void OnGUI()
    {
        m_CurrentProperty = m_serializedObject.FindProperty("levels");
        DrawProperties(m_CurrentProperty,true);
        curveX = EditorGUILayout.CurveField("Animation on X", curveX);

        if (GUILayout.Button("Generate Config"))
            GenerateConfig();
    }
    void GenerateConfig()
    {
        List<LevelConfig> selectedLevels = new List<LevelConfig>();
        string path = "Assets/BMS+ NewFace/levels.asset";
        foreach (Keyframe key in curveX.keys)
        {
            selectedLevels.Add(GetNearestLevel(key));
        }
        LevelsConfig resultConfig = new LevelsConfig();
        resultConfig.levels = selectedLevels;
        Debug.Log(resultConfig);
        Debug.Log(resultConfig.levels);
        AssetDatabase.CreateAsset(resultConfig, path);
    }
    public LevelConfig GetNearestLevel(Keyframe key)
    {
        float smallestDistance = float.MaxValue;
        LevelConfig nearestLevel = null;
        foreach(LevelConfig level in m_LevelsConfig.levels)
        {
            if (Math.Abs( level.dificaulty - key.value) < smallestDistance)
            {
                smallestDistance = Math.Abs(level.dificaulty - key.value);
                nearestLevel = level;
            }
        }
        if (nearestLevel == null)
        {
            Debug.LogError("LevelConfigError: null level");
        }
        return nearestLevel;
    }

}
