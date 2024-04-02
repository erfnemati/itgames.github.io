using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public  class AssetHandler
{
    [OnOpenAsset]
    public static bool OpenEditorWindow(int instanceId,int line)
    {
        LevelsConfig obj = EditorUtility.InstanceIDToObject(instanceId) as LevelsConfig;
        if (obj != null )
        {
            LevelPickerEditor.OpenEditor(obj);
            return true;
        }
        return false;
    }
}
public class LevelsConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Editor"))
        {
            LevelPickerEditor.OpenEditor((LevelsConfig)target);
        }
    }
}
