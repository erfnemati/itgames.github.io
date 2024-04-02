using LevelDesign;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EditorShapeManager))]
public class EditorShapeWindow : Editor
{
    private Rect windowRect = new Rect(20, 20, 120, 50);
    private EditorShapeManager shapeManager;
    private void Awake()
    {
        shapeManager = (EditorShapeManager)target;
    }

    private void OnSceneGUI()
    {
        if (LevelDesignBoard._instance.phase==LevelDesignPhase.Phase4)
            windowRect = GUILayout.Window(0, windowRect, WindowFunction, "My Window");
    }

    private void WindowFunction(int windowID)
    {
        List<ShapeColorData> colors = EditorDataManager._instance.GetData<ShapeColorConfig>().shapeColors;
        foreach (ShapeColorData color in colors)
        {
            if (GUILayout.Button(color.name.ToString()))
            {
                SetShapeColor(color);
            }

        }
        shapeManager.shapeData.shapeAddedNumber = EditorGUILayout.IntField("Number OF Collors", 
            shapeManager.shapeData.shapeAddedNumber);


        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }
    private void SetShapeColor(ShapeColorData color)
    {
        target.GetComponentsInChildren<SpriteRenderer>()[1].sprite=color.sprite;
        shapeManager.SetColor(color.color);
    }
}

//public class CustomTools : EditorWindow
//{
//    [MenuItem("Window/Custom Tools/Enable")]
//    public static void Enable()
//    {
//        SceneView.duringSceneGui += OnSceneGUI;
//    }

//    [MenuItem("Window/Custom Tools/Disable")]
//    public static void Disable()
//    {
//        SceneView.duringSceneGui -= OnSceneGUI;
//    }

//    private static void OnSceneGUI(SceneView sceneview)
//    {
//        Handles.BeginGUI();
//        if (GUILayout.Button("Button"))
//            Debug.Log("Button Clicked");
//        Handles.EndGUI();
//    }
//}
