using System.Collections.Generic;
using UnityEngine;
using GameEnums;
using GameData;
using System.Diagnostics.Tracing;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
public class LevelConfigData : ScriptableObject
{
    public GameMode gameMode;//{ get; private set; } // [q] how to handle this?
    public ShapeType shapeType;
    public float dificaulty;//{ get; private set; }
    public float levelTime;
    
    public boardData<PinData> pins;

    public boardData<ShapeData> GameBoardCanvas; //{ get; private set; }

    public boardData<ShapeData> ReferenceGameBoard; //{ get; private set; }

    public List<PinPointData> gameBoardPinPoints; //{ get; private set; }

    public GuideCanvasData GuideCanvas; //{ get; private set; }

    public List<EventData> events;
    public void SetLevelConfigData(boardData<PinData> pins,List<PinPointData> gamePinPoints
        ,boardData<ShapeData> gameBoardCanvas, boardData<ShapeData> referenceGameBoard
        ,GuideCanvasData guideCanvas,GameMode gameMode,float dificaulty, List<EventData> events) 
    { 
        this.pins = pins;
        this.gameBoardPinPoints = gamePinPoints;
        this.GuideCanvas= guideCanvas;
        this.GameBoardCanvas= gameBoardCanvas;
        this.ReferenceGameBoard= referenceGameBoard;
        this.dificaulty= dificaulty;
        this.gameMode   = gameMode;
        this.events = events;
    }


}
