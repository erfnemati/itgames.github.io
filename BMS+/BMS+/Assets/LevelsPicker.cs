using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class LevelsPicker : MonoBehaviour
{
    [SerializeField] bool enableLevelPicker = true;
    [SerializeField] List<AnimationCurve> collectionCurves = new List<AnimationCurve>();
    [SerializeField] float randomOffset;
    [SerializeField] LevelsConfig m_LevelsConfig;
    private List<LevelsConfig> SelectedLevels;
    void Start()
    {
        if( enableLevelPicker)
        {
            SelectedLevels = new List<LevelsConfig>();
            foreach (AnimationCurve curve in collectionCurves)
            {
                SelectedLevels.Add(GenerateConfig(curve));
            }
            System.Random rand = new System.Random();
            int index = rand.Next(SelectedLevels.Count);
            ServiceLocator._instance.Get<BmsPlusSceneManager>().SetLevels( SelectedLevels[index].levels);
        }
        else
            ServiceLocator._instance.Get<BmsPlusSceneManager>().SetLevels(m_LevelsConfig.levels);

    }
    LevelsConfig GenerateConfig(AnimationCurve collection)
    {
        List<LevelConfigData> selectedLevels = new List<LevelConfigData>();
        foreach (Keyframe key in collection.keys)
        {
            selectedLevels.Add(GetNearestLevel(key));
        }
        LevelsConfig resultConfig = new LevelsConfig();
        resultConfig.levels = selectedLevels;
        return resultConfig;

    }
    public LevelConfigData GetNearestLevel(Keyframe key)
    {
        List<LevelConfigData> nearestCollection = new List<LevelConfigData>();
        foreach (LevelConfigData level in m_LevelsConfig.levels)
        {
            if (Math.Abs(level.dificaulty - key.value) < randomOffset)
            {
                nearestCollection.Add(level);
            }
        }
        if (nearestCollection.Count != 0)
        {
            System.Random rand = new System.Random();
            int index = rand.Next(nearestCollection.Count);
            return nearestCollection[index];
        }
        else
        {
                        Debug.LogError("LevelConfigError: null level");

            System.Random rand = new System.Random();
            int index = rand.Next(m_LevelsConfig.levels.Count);
            return m_LevelsConfig.levels[index];
        }
    }
}
