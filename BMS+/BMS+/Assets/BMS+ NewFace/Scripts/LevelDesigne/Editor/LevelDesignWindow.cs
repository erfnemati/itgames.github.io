using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using LevelDesign;
using GameEnums;
using System;

[CustomEditor(typeof(LevelDesignBoard))]
public class LevelDesignWindow : Editor
{
    LevelDesignBoard board;
    ShapeGenerator shapeGenerator;
    private Rect windowRect = new Rect(20, 20, 120, 50);


    private void Awake()
    {
        board = (LevelDesignBoard)target;
        shapeGenerator = new ShapeGenerator(board);
    }
    private void OnSceneGUI()
    {
        windowRect = GUILayout.Window(0, windowRect, WindowFunction, "My Window");
    }

    private void WindowFunction(int windowID)
    {
        if (board.phase == LevelDesignPhase.Phase1)
            ShowInitialWindow();
        else if (board.phase == LevelDesignPhase.Phase2)
            ShowLevelWindow();
        else if (board.phase == LevelDesignPhase.Phase3)
            ShowGameBoardWindow();
        else if (board.phase == LevelDesignPhase.Phase4)
            ShowSaveWindow();



        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }

    private void ShowInitialWindow()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Level"))
        {
            board.LoadLevel();
            board.phase = LevelDesignPhase.Phase4;
        }
        if (GUILayout.Button("Create New Level"))
        {
            board.CreateNewLevel();
            board.phase = LevelDesignPhase.Phase2;
        }
        GUILayout.EndHorizontal();
    }
    private void ShowLevelWindow()
    {
        GUILayout.BeginVertical();
        GUILayout.Label("Pins", EditorStyles.boldLabel);
        for (int i = 0; i < board.pinsChecker.Count; i++)
        {
            board.pinsChecker[i] = EditorGUILayout.Toggle(((PinName)i).ToString(), board.pinsChecker[i]);
        }
        GUILayout.Label("GuideCanvas", EditorStyles.boldLabel);
        board.selectedGuideCanvasName= (GuideCanvasName)EditorGUILayout.EnumPopup("Choose GuideCanvas Type", board.selectedGuideCanvasName);

        if(GUILayout.Button("Proceed"))
        {
            board.InstantiateLevelWindowObjects();
            board.phase = LevelDesignPhase.Phase3;

        }
        GUILayout.EndVertical();
    }
    private void ShowGameBoardWindow()
    {
        GUILayout.BeginVertical();
        board.level.shapeType = (ShapeType)EditorGUILayout.EnumPopup("Choose Shape Type", board.level.shapeType);
        GUILayout.BeginHorizontal();
        board.gameBoardSize.x = EditorGUILayout.IntField("Enter a number", (int)board.gameBoardSize.x);
        board.gameBoardSize.y = EditorGUILayout.IntField("Enter a number", (int)board.gameBoardSize.y);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate Board"))
        {
            GenerateBoardShapes(board.level.shapeType);
            board.isBoardgenerated = true;
        }
        if (GUILayout.Button("Set PinPoint Neighbor Shapes"))
        {
            shapeGenerator.AddPinPointNeighbors();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        board.level.gameMode = (GameMode)EditorGUILayout.EnumPopup("Choose Shape Type", board.level.gameMode);
        if (GUILayout.Button("Proceed"))
        {
            board.phase = LevelDesignPhase.Phase4;
            shapeGenerator.InitializeReferenceBoard();
        }
        GUILayout.EndHorizontal ();
        GUILayout.EndVertical();
    }
    private void ShowSaveWindow()
    {
        switch (board.level.gameMode)
        {
            case GameMode.Normal:
                if(GUILayout.Button("Save"))
                {
                    board.phase = LevelDesignPhase.Phase1;
                    SaveDefaultToConfig();
                }
                break;
            case GameMode.Blitz:
                if (GUILayout.Button("Proceed To Events"))
                {
                }
                break;
            case GameMode.HalfWayThere:
                GUILayout.BeginHorizontal();
                if(GUILayout.Button(" Save Initial Board "))
                {
                    SaveDefaultToConfig();
                }
                if(GUILayout.Button("Save Goal Board"))
                {
                    board.SaveToConfig(board.referenceBoard.transform, board.referenceShapeManagerList);
                }
                GUILayout.EndHorizontal();
                break;
            default:
                SaveDefaultToConfig();
                break;
        }
    }

    private void SaveDefaultToConfig()
    {
        board.SaveToConfig(board.GetComponent<RectTransform>(), board.shapeManagerList);
        board.SaveToConfig(board.pinPointList);
        board.SaveToConfig(board.pinPlaceholder, board.pinList);
        board.SaveToConfig(board.guideCanvas);
        board.SaveToConfig(board.referenceBoard.transform,board.referenceShapeManagerList);
    }

    public void GenerateBoardShapes(ShapeType type)
    {
        switch(type)
        {
            case ShapeType.rectangle:
                shapeGenerator.GenerateRectangles();
                break;
            case ShapeType.Triangle:
                shapeGenerator.GenerateTriangles();
                break;
            case ShapeType.Hexagon:
                shapeGenerator.GenerateHexagons();
                break;
        }
    }

}
