using ConfigData;
using GameData;
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
    float occuranceTime = 0f;
    int shapeAddedNumber = 0;
    VectorInt eventColor;
    private void Awake()
    {
        shapeManager = (EditorShapeManager)target;
    }

    private void OnSceneGUI()
    {
        if (LevelDesignBoard._instance.phase==LevelDesignPhase.Phase5)
        {
            windowRect = GUILayout.Window(0, windowRect, WindowFunction, "My Window");
        }
    }

    private void WindowFunction(int windowID)
    {
        List<ShapeConfigData> colors = LevelDesignBoard._instance.GetData<ShapeConfig>().shapeColors;

        GUILayout.BeginHorizontal();
        foreach (ShapeConfigData color in colors)
        {
            if (GUILayout.Button(color.name.ToString()))
            {
                SetShapeColor(color);
            }

        }
        GUILayout.EndHorizontal();

        shapeManager.shapeEvent.time = EditorGUILayout.FloatField("Occurance TIme", shapeManager.shapeEvent.time);
        shapeManager.shapeEvent.shapeAddedNumber = EditorGUILayout.IntField("Number OF Collors",
            shapeManager.shapeEvent.shapeAddedNumber);

        if(GUILayout.Button("Add Event"))
        {
            shapeManager.SetEventData(eventColor, occuranceTime, shapeAddedNumber);
            shapeManager.EventAddedNumber(shapeAddedNumber);
        }
        if (GUILayout.Button("Save Events"))
        {
            shapeManager.SaveEventData();
            LevelDesignBoard._instance.SaveEventsToConfig();
        }

        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }
     void SetShapeColor(ShapeConfigData color)
    {
        target.GetComponentsInChildren<SpriteRenderer>()[1].sprite=color.sprite;
        eventColor=color.color;
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
