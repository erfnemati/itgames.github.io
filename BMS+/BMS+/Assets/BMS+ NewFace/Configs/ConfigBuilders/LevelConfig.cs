using System.Collections.Generic;
using UnityEngine;
using GameEnums;
using GameData;
using System.Diagnostics.Tracing;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public GameMode gameMode;//{ get; private set; } // [q] how to handle this?
    public ShapeType shapeType;
    public float dificaulty;//{ get; private set; }
    public Vector3 PinPlaceholder; //{ get; private set; }
    public List<PinData> pins; //{ get; private set; }

    public RectTransform GameBoardCanvas; //{ get; private set; }
    public List<ShapeData> shapes; //{  get; private set; }

    public Transform ReferenceGameBoard; //{ get; private set; }
    public List<ShapeData> refrenceShapes; //{ get; private set; }

    public List<PinPointData> gameBoardPinPoints; //{ get; private set; }

    public GuideCanvasData GuideCanvas; //{ get; private set; }

    public List<EventData> events;
    public void SetLevelConfigData(List<PinData> pins, List<ShapeData> shapes, List<ShapeData> refrenceShapes,
        List<PinPointData> gamePinPoints, Vector3 pinPlaceHolder, RectTransform gameBoardCanvas,Transform referenceGameBoard,
        GuideCanvasData guideCanvas,GameMode gameMode,float dificaulty, List<EventData> events) 
    { 
        this.pins = pins;
        this.shapes = shapes;
        this.refrenceShapes = refrenceShapes;
        this.gameBoardPinPoints = gamePinPoints;
        this.PinPlaceholder= pinPlaceHolder;
        this.GuideCanvas= guideCanvas;
        this.GameBoardCanvas= gameBoardCanvas;
        this.ReferenceGameBoard= referenceGameBoard;
        this.dificaulty= dificaulty;
        this.gameMode   = gameMode;
        this.events = events;
    }


}
